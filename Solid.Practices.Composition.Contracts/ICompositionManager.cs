using System.Collections.Generic;
using System.Reflection;

namespace Solid.Practices.Composition.Contracts
{    
    /// <summary>
    /// Allows initializing composition modules using pre-loaded assemblies.
    /// </summary>
    public interface ICompositionManager : ICompositionModulesProvider
    {
        /// <summary>
        /// Initializes composition modules using pre-loaded assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>        
        void Initialize(IEnumerable<Assembly> assemblies);
    }
}
