using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

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
            UpdateManifestFiles(prefix, versionInfo);
        }

        private static void UpdateManifestFiles(string prefix, VersionInfo versionInfo)
        {
            GoUp(3);
            Cd("pack");
            var version = versionInfo.ToString();
            var manifestFiles =
                Directory.GetFiles(Directory.GetCurrentDirectory(), $"*{prefix}*.nuspec", SearchOption.AllDirectories);
            foreach (var manifestFile in manifestFiles)
            {
                var doc = new XmlDocument();
                doc.Load(manifestFile);
                var versionElement = doc.GetElementsByTagName("package")[0]["metadata"]["version"];
                versionElement.InnerText = version;
                var dependenciesElement = doc.GetElementsByTagName("package")[0]["metadata"]["dependencies"];
                if (dependenciesElement != null)
                {
                    var dependencies = dependenciesElement.GetElementsByTagName("dependency");
                    foreach (XmlNode dependencyElement in dependencies)
                    {
                        if (dependencyElement.Attributes["id"].Value.StartsWith(prefix))
                        {
                            dependencyElement.Attributes["version"].Value = version;
                        }
                    }
                }
                doc.Save(manifestFile);
            }
            GoUp(1);
            Cd("update");
            Cd("UpdateUtil");
            Cd("bin");
        }

        private static void GoUp(int numberOfLevels)
        {
            var relativePath = new List<string> { Directory.GetCurrentDirectory() };
            relativePath.AddRange(Enumerable.Repeat("..", numberOfLevels));
            Directory.SetCurrentDirectory(Path.Combine(relativePath.ToArray()));
        }

        private static void Cd(string directory)
        {
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), directory));
        }
    }

    class VersionInfo
    {
        public VersionInfo(string version)
        {
            var versionParts = version.Split(new[] { '-' });
            VersionCore = versionParts[0];
            PreRelease = versionParts.Length > 1 ? versionParts[1] : null;
        }

        public string VersionCore { get;  }
        public string PreRelease { get; }

        public override string ToString()
        {
            return PreRelease == null ? VersionCore : $"{VersionCore}-{PreRelease}";
        }
    }
}
