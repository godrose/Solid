﻿using System.IO;

namespace Solid.Common
{
    /// <summary>
    /// Implementation of <see cref="IPlatformProvider"/> for .NETStandard
    /// </summary>
    public class NetStandardPlatformProvider : PlatformProviderBase
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path) => Directory.GetFiles(path);

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path, string searchPattern) => Directory.GetFiles(path, searchPattern);

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns></returns>
        public override string GetRootPath() => Directory.GetCurrentDirectory();

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>        
        public override void WriteText(string path, string contents)
        {
            var fileStream = new FileStream(path, FileMode.Create);
            using (var textWriter = new StreamWriter(fileStream))
            {
                textWriter.Write(contents);
                textWriter.Flush();
            }
        }

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>        
        public override string ReadText(string path)
        {
            var fileStream = new FileStream(path, FileMode.Open);
            using (var textReader = new StreamReader(fileStream))
            {
                var contents = textReader.ReadToEnd();
                return contents;
            }
        }
    }
}
