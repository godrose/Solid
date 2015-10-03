using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents means of initializing bootstrapper from the given path
    /// </summary>
    public interface IBootstrapperInitializationFacade
    {
        /// <summary>
        /// Assemblies resolver
        /// </summary>
        IAssembliesReadOnlyResolver AssembliesResolver { get; }

        /// <summary>
        /// Collection of composition modules
        /// </summary>
        IEnumerable<ICompositionModule> Modules { get; }

        /// <summary>
        /// Initialized bootstrapper from the provided path
        /// </summary>
        /// <param name="rootPath">Root path</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates</param>
        void Initialize(string rootPath, string[] prefixes = null);
    }
}
