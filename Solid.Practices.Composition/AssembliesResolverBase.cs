using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Base class for assembly resolvers.
    /// </summary>
    public abstract class AssembliesResolverBase : IAssembliesReadOnlyResolver
    {
        private readonly IAssemblySourceProvider _assemblySourceProvider;
                
        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolverBase"/> class.
        /// </summary>
        protected AssembliesResolverBase(IAssemblySourceProvider assemblySourceProvider)
        {
            _assemblySourceProvider = assemblySourceProvider;            
        }

        /// <summary>
        /// Override this method to retrieve platform-specific root assemblies.
        /// </summary>
        /// <returns>Collection of assemblies.</returns>
        protected abstract IEnumerable<Assembly> GetRootAssemblies();

        /// <summary>
        /// Gets available assemblies.
        /// </summary>
        /// <returns>Collection of assemblies.</returns>
        IEnumerable<Assembly> IAssembliesReadOnlyResolver.GetAssemblies()
        {
            var assemblies = GetRootAssemblies();
            return assemblies.Concat(_assemblySourceProvider.Assemblies).Distinct();
        }
    }
}