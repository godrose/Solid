using System.Collections.Generic;
using System.Reflection;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents an object that is able to retrieve list of the assemblies to be 
    /// inspected for application elements.
    /// </summary>
    public interface IAssemblySourceProvider
    {
        /// <summary>
        /// Gets the assemblies to be inspected.
        /// </summary>
        /// <value>
        /// The assemblies to be inspected.
        /// </value>
        IEnumerable<Assembly> InspectedAssemblies { get; }
    }
}