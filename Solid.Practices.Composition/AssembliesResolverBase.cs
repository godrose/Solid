using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    public abstract class AssembliesResolverBase : IAssembliesReadOnlyResolver
    {
        private readonly ICompositionModulesProvider _compositionModulesProvider;

        protected AssembliesResolverBase(ICompositionModulesProvider compositionModulesProvider)
        {
            _compositionModulesProvider = compositionModulesProvider;
        }

        protected abstract IEnumerable<Assembly> GetRootAssemblies();

        public IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = GetRootAssemblies();
            return
                assemblies.Concat(_compositionModulesProvider.Modules != null
                    ? _compositionModulesProvider.Modules.Select(t => t.GetType().Assembly)
                    : new Assembly[] {}).Distinct();
        }
    }
}