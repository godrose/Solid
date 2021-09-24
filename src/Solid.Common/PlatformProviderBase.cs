using System.IO;

namespace Solid.Common
{
    /// <summary>
    /// Contains default implementation of some <see cref="IPlatformProvider"/> methods.
    /// </summary>
    public abstract class PlatformProviderBase : IPlatformProvider
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public abstract string[] GetFiles(string path);

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public abstract string[] GetFiles(string path, string searchPattern);

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns></returns>
        public abstract string GetRootPath();

        /// <summary>
        /// Gets the absolute path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns></returns>
        public string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(GetRootPath(), relativePath);
        }

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>
        public abstract void WriteText(string path, string contents);

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public abstract string ReadText(string path);
    }
}