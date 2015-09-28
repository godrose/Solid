using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition.Desktop
{
    /// <summary>
    /// Assemblies resolver for client part of desktop applications
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase
    {
        private readonly Type _entryType;

        public AssembliesResolver(Type entryType,
            ICompositionModulesProvider compositionModulesProvider) : base(compositionModulesProvider)
        {
            _entryType = entryType;
        }

        protected override IEnumerable<Assembly> GetRootAssemblies()
        {
            return _entryType.Assembly
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Concat(new[] { _entryType.Assembly })
                .Distinct();
        }
    }
}
