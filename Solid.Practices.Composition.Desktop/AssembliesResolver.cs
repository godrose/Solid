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

        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolver"/> class.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="compositionModulesProvider">The composition modules provider.</param>
        public AssembliesResolver(Type entryType,
            ICompositionModulesProvider compositionModulesProvider) : base(compositionModulesProvider)
        {
            _entryType = entryType;
        }

        /// <summary>
        /// Override this method to retrieve platform-specific root assemblies
        /// </summary>
        /// <returns>Collection of assemblies</returns>
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
