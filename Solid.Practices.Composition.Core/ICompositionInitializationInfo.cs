using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents composition initialization information.
    /// </summary>
    public interface ICompositionInitializationInfo
    {
        /// <summary>
        /// Assemblies resolver
        /// </summary>
        IAssembliesReadOnlyResolver AssembliesResolver { get; }

        /// <summary>
        /// Collection of composition modules
        /// </summary>
        IEnumerable<ICompositionModule> Modules { get; }
    }
}