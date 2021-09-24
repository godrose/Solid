using System;
using System.IO;
using System.Threading;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Solid.Common
{
    class FileReader : IDisposable
    {
        private ManualResetEventSlim _mre;
        private ManualResetEventSlim _mre2;

        public void Read(string path)
        {
            _mre = new ManualResetEventSlim();
            _mre2 = new ManualResetEventSlim();
            var fromPathAsync = StorageFile.GetFileFromPathAsync(path);
            fromPathAsync.Completed += Completed;
            while (_mre.IsSet == false)
            {
                _mre.Wait();
            }
            _mre.Reset();
            _mre2.Reset();
        }

        private void Completed(IAsyncOperation<StorageFile> asyncInfo, AsyncStatus asyncStatus)
        {
            var file = asyncInfo.GetResults();
            var asyncOperation = file.OpenAsync(FileAccessMode.Read);
            asyncOperation.Completed += Completed;
            while (_mre2.IsSet == false)
            {
                _mre2.Wait();
            }
            _mre.Set();
        }

        private void Completed(IAsyncOperation<IRandomAccessStream> asyncInfo, AsyncStatus asyncStatus)
        {
            var stream = asyncInfo.GetResults();
            var readStream = stream.AsStreamForRead();           
            using (var streamReader = new StreamReader(readStream))
            {
                Contents = streamReader.ReadToEnd();
            }            
            stream.Dispose();
            _mre2.Set();
        }

        internal string Contents { get; private set; }

        private void ReleaseUnmanagedResources()
        {
            _mre.Dispose();
            _mre2.Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~FileReader()
        {
            ReleaseUnmanagedResources();
        }
    }
}
