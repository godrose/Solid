using System.Collections.Generic;
using System.Reflection;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents assemblies resolver.
    /// </summary>
    public interface IAssembliesReadOnlyResolver
    {
        /// <summary>
        /// Gets available assemblies.
        /// </summary>
        /// <returns>Collection of assemblies.</returns>
        IEnumerable<Assembly> GetAssemblies();
    }
}