using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Platform-specific implementation of <see cref="IPlatformProvider"/>
    /// </summary>    
    public class
#if NET45
        NetPlatformProvider
#endif
#if NETFX_CORE || WINDOWS_UWP
        UniversalPlatformProvider
#endif
        : IPlatformProvider
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public string[] GetFiles(string path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public string[] GetFiles(string path, string searchPattern)
        {
            return System.IO.Directory.GetFiles(path, searchPattern);
        }
    }
}
