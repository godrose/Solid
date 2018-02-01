using System.Collections.Generic;
using System.Reflection;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents assembly loading strategy.
    /// </summary>
    public interface IAssemblyLoadingStrategy
    {
        /// <summary>
        /// Loads the assemblies.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> Load();
    }
}