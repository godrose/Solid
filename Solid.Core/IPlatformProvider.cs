namespace Solid.Core
{
    /// <summary>
    /// Interface for platform specific operations that need enlightenment.
    /// </summary>
    public interface IPlatformProvider
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string[] GetFiles(string path);

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        string[] GetFiles(string path, string searchPattern);

        /// <summary>
        /// Gets the root path.
        /// </summary>
        /// <returns></returns>
        string GetRootPath();

        /// <summary>
        /// Gets the absolute path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns></returns>
        string GetAbsolutePath(string relativePath);

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>
        void WriteText(string path, string contents);

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string ReadText(string path);
    }
}