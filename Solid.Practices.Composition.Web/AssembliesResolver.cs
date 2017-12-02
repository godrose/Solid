using System.Collections.Generic;
using System.Reflection;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Assemblies resolver.
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolver"/> class.
        /// </summary>        
        /// <param name="assemblySourceProvider">The assembly source provider.</param>
        public AssembliesResolver(IAssemblySourceProvider assemblySourceProvider) : base(assemblySourceProvider)
        {

        }

        /// <summary>
        /// Override this method to retrieve platform-specific root assemblies.
        /// </summary>
        /// <returns>Collection of assemblies.</returns>
        protected override IEnumerable<Assembly> GetRootAssemblies()
        {
            return new Assembly[] { };
        }
    }
}
