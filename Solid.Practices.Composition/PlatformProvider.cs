using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Ambient context that holds current value for <see cref="IPlatformProvider"/>.
    /// </summary>
    public class PlatformProvider
    {
        /// <summary>
        /// Gets or sets the current value for platform provider.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static IPlatformProvider Current { get; set; } = new DefaultPlatformProvider();
    }

    /// <summary>
    /// Default implementation of <see cref="IPlatformProvider"/>.
    /// </summary>
    /// <seealso cref="IPlatformProvider" />
    public class DefaultPlatformProvider : PlatformProviderBase
    {
        /// <summary>
        /// Gets the files at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path) => new string[] {};

        /// <summary>
        /// Gets the files at the specified path, using provided search pattern.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path, string searchPattern) => new string[] { };

        /// <summary>
        /// Gets the root directory.
        /// </summary>
        /// <returns></returns>
        public override string GetRootPath() => string.Empty;

        /// <summary>
        /// Writes the specified text into the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The text.</param>
        public override void WriteText(string path, string contents)
        {
            
        }

        /// <summary>
        /// Reads the contents of the resource identified by the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string ReadText(string path) => string.Empty;
    }
}
