#if NET45 || WINDOWS_UWP
using System.IO;
#endif
#if WINRT
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
#endif
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IPlatformProvider"/>.
    /// </summary>    
    public class
#if NET45
        NetPlatformProvider
#endif
#if WINDOWS_UWP
        UniversalPlatformProvider
#endif
#if WINRT
        WinRTPlatformProvider
#endif
        : PlatformProviderBase
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path)
        {
#if WINRT
            var taskResult = GetFilesInternal(path);
            return taskResult.Result;
#else
            return Directory.GetFiles(path);
#endif
        }

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path, string searchPattern)
        {
#if WINRT
            var taskResult = GetFilesInternal(path);
            return taskResult.Result.Where(t => IsMatch(t, searchPattern)).ToArray();
#else      
            return System.IO.Directory.GetFiles(path, searchPattern);
#endif
        }

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns></returns>
        public override string GetRootPath()
        {
#if WINRT
            return Package.Current.InstalledLocation.Path;            
#else
            return Directory.GetCurrentDirectory();
#endif
        }

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>        
        public override
#if WINRT
            async
#endif
            void WriteText(string path, string contents)
        {
#if WINRT
            //supports only current folder
            var folder = Package.Current.InstalledLocation;
            var file = await folder.CreateFileAsync(path);
            await FileIO.WriteTextAsync(file, contents);
#else
            var fileStream = new FileStream(path, FileMode.Create);
            using (var textWriter = new StreamWriter(fileStream))             
            {
                textWriter.Write(contents);
                textWriter.Flush();
            }           
#endif
        }

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>        
        public override string ReadText(string path)
        {
#if WINRT
            //support only current folder
            var folder = Package.Current.InstalledLocation;
            var fileOperation = folder.GetFileAsync(path);
            while (fileOperation.Status != AsyncStatus.Completed && fileOperation.Status != AsyncStatus.Canceled &&
                   fileOperation.Status != AsyncStatus.Error)
            {
                Task.Delay(50);
            }
            var file = fileOperation.GetResults();
            var contentsOperation = FileIO.ReadTextAsync(file);
            while (contentsOperation.Status != AsyncStatus.Completed && contentsOperation.Status != AsyncStatus.Canceled &&
                   contentsOperation.Status != AsyncStatus.Error)
            {
                Task.Delay(50);
            }
            return contentsOperation.GetResults();
#else
            var fileStream = new FileStream(path, FileMode.Open);
            using (var textReader = new StreamReader(fileStream))
            {
                var contents = textReader.ReadToEnd();
                return contents;
            }                          
#endif
        }

#if WINRT

        private const int Delay = 50;

        private static async Task<string[]> GetFilesInternal(string path)
        {
            path = path.TrimEnd('.');
            var folderOperation = StorageFolder.GetFolderFromPathAsync(path);
            while (folderOperation.Status == AsyncStatus.Started)
            {
                await Task.Delay(Delay);                
            }
            if (folderOperation.Status == AsyncStatus.Error)
            {
                throw folderOperation.ErrorCode;
            }
            var folder = folderOperation.GetResults();
            var filesOperation = folder.GetFilesAsync();
            while (filesOperation.Status == AsyncStatus.Started)
            {
                await Task.Delay(Delay);
            }
            if (filesOperation.Status == AsyncStatus.Error)
            {
                throw filesOperation.ErrorCode;
            }
            var files = filesOperation.GetResults();
            return files.Select(t => t.Name).ToArray();
        }

        private bool IsMatch(string name, string pattern)
        {            
            Regex regex = FindFilesPatternToRegex.Convert(pattern);
            var isMatch = regex.IsMatch(name);
            return isMatch;
        }

        private static class FindFilesPatternToRegex
        {
            private static Regex HasQuestionMarkRegEx = new Regex(@"\?", RegexOptions.None);
            private static Regex IllegalCharactersRegex = new Regex("[" + @"\/:<>|" + "\"]", RegexOptions.None);
            private static Regex CatchExtensionRegex = new Regex(@"^\s*.+\.([^\.]+)\s*$", RegexOptions.None);
            private static string NonDotCharacters = @"[^.]*";
            public static Regex Convert(string pattern)
            {
                if (pattern == null)
                {
                    throw new ArgumentNullException();
                }
                pattern = pattern.Trim();
                if (pattern.Length == 0)
                {
                    throw new ArgumentException("Pattern is empty.");
                }
                if (IllegalCharactersRegex.IsMatch(pattern))
                {
                    throw new ArgumentException("Pattern contains illegal characters.");
                }
                bool hasExtension = CatchExtensionRegex.IsMatch(pattern);
                bool matchExact = false;
                if (HasQuestionMarkRegEx.IsMatch(pattern))
                {
                    matchExact = true;
                }
                else if (hasExtension)
                {
                    matchExact = CatchExtensionRegex.Match(pattern).Groups[1].Length != 3;
                }
                string regexString = Regex.Escape(pattern);
                regexString = "^" + Regex.Replace(regexString, @"\\\*", ".*");
                regexString = Regex.Replace(regexString, @"\\\?", ".");
                if (!matchExact && hasExtension)
                {
                    regexString += NonDotCharacters;
                }
                regexString += "$";
                Regex regex = new Regex(regexString, RegexOptions.None | RegexOptions.IgnoreCase);
                return regex;
            }
        }
#endif
    }
}
