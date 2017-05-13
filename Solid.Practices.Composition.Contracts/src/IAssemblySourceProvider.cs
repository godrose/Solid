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
        /// Gets the assemblies which can be inspected for the additional components.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        IEnumerable<Assembly> Assemblies { get; }
    }
}