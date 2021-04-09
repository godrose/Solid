using System;
using System.Collections.Generic;

namespace UpdateUtil
{
    static class Program
    {
        static void Main(string[] args)
        {
            var command = args[0];
            var prefix = args[1];
            var version = args.Length >= 3 ? args[2] : string.Empty;
            if (version == string.Empty)
            {
                Console.WriteLine("No version provided");
                return;
            }
            var versionInfo = new VersionInfo(version);
            var handlers = new List<FileTypeHandlerBase>();
            switch (command)
            {
                case "bump-version":
                    handlers.Add(new SdkProjectFileTypeHandler());
                    handlers.Add(new AssemblyInfoFileTypeHandler());
                    handlers.Add(new CIFileTypeHandler());
                    handlers.Add(new ManifestFileTypeHandler(new ManifestFileTypeHandlerOptions 
                    {
                        UpdatePackageVersion = true,
                        UpdateDependencyVersion = true
                    }));
                    break;
                case "bump-dependency-version":                                                            
                    handlers.Add(new ManifestFileTypeHandler(new ManifestFileTypeHandlerOptions 
                    {
                        UpdateDependencyVersion = true
                    }));
                    break;
                default:
                    break;
            }           
            foreach (var handler in handlers)
            {
                handler.UpdateFiles(prefix, versionInfo);
            }
        }
    }
}
