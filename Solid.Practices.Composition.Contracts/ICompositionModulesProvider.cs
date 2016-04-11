using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents strongly-type read-only collection of composition modules.
    /// </summary>
    public interface ICompositionModulesProvider : ICompositionModulesProvider<ICompositionModule>
    {
    }

    /// <summary>
    /// Represents a read-only collection of composition modules.
    /// </summary>
    /// <typeparam name="TModule">Type of composition module.</typeparam>
    public interface ICompositionModulesProvider<TModule>
    {
        /// <summary>
        /// Collection of composition modules.
        /// </summary>
        IEnumerable<TModule> Modules { get; }
    }

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
        /// Gets the current directory.
        /// </summary>
        /// <returns></returns>
        string GetCurrentDirectory();
    }
}
