using System;

namespace UpdateUtil
{
    static class Program
    {
        static void Main(string[] args)
        {
            var prefix = args[0];
            var version = args.Length >= 2 ? args[1] : string.Empty;
            if (version == string.Empty)
            {
                Console.WriteLine("No version provided");
                return;
            }
            var versionInfo = new VersionInfo(version);
            var handlers = new FileTypeHandlerBase[]
            {
                new SdkProjectFileTypeHandler(), 
                new AssemblyInfoFileTypeHandler(), 
                new CIFileTypeHandler(),
                new ManifestFileTypeHandler()
            };
            foreach (var handler in handlers)
            {
                handler.UpdateFiles(prefix, versionInfo);
            }
        }
    }
}
