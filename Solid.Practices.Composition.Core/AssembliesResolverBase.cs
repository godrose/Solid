using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Base class for assembly resolvers
    /// </summary>
    public abstract class AssembliesResolverBase : IAssembliesReadOnlyResolver
    {
        private readonly ICompositionModulesProvider _compositionModulesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolverBase"/> class.
        /// </summary>
        /// <param name="compositionModulesProvider">The composition modules provider.</param>
        protected AssembliesResolverBase(ICompositionModulesProvider compositionModulesProvider)
        {
            _compositionModulesProvider = compositionModulesProvider;
        }

        /// <summary>
        /// Override this method to retrieve platform-specific root assemblies
        /// </summary>
        /// <returns>Collection of assemblies</returns>
        protected abstract IEnumerable<Assembly> GetRootAssemblies();

        /// <summary>
        /// Gets available assemblies
        /// </summary>
        /// <returns>Collection of assemblies</returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = GetRootAssemblies();
            return
                assemblies.Concat(_compositionModulesProvider.Modules != null
                    ? _compositionModulesProvider.Modules.Select(t => t.GetType().GetTypeInfo().Assembly)
                    : new Assembly[] {}).Distinct();
        }
    }
}