using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents strongly-type read-only collection of composition modules
    /// </summary>
    public interface ICompositionModulesProvider : ICompositionModulesProvider<ICompositionModule>
    {
    }

    /// <summary>
    /// Represents a read-only collection of composition modules
    /// </summary>
    /// <typeparam name="TModule">Type of composition module</typeparam>
    public interface ICompositionModulesProvider<TModule>
    {
        /// <summary>
        /// Collection of composition modules
        /// </summary>
        IEnumerable<TModule> Modules { get; }
    }
}
