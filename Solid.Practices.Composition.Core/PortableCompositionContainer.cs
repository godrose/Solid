using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Reflection;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents composition container which allows composing the composition modules
    /// from the provided list of assemblies.
    /// </summary>
    /// <typeparam name="TModule">The type of composition module.</typeparam>
    public class PortableCompositionContainer<TModule> : ICompositionContainer<TModule>
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortableCompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>        
        public PortableCompositionContainer(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;            
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>    
        [ImportMany]    
        public IEnumerable<TModule> Modules { get; private set; }

        void ICompositionContainer<TModule>.Compose()
        {
            var containerConfiguration = new ContainerConfiguration();
            containerConfiguration.WithAssemblies(_assemblies);
            using (var compostionHost = containerConfiguration.CreateContainer())
            {
                compostionHost.SatisfyImports(this);                
            }
        }
    }
}
