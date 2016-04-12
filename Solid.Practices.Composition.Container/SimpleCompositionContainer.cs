using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Container
{
    /// <summary>
    /// Represents a basic implementation of composition container with export feature only.
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="ICompositionContainer" />
    public class SimpleCompositionContainer<TModule> : ICompositionContainer<TModule>   
        where TModule : ICompositionModule     
    {
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly IModuleCreationStrategy _moduleCreationStrategy;        

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>        
        /// <param name="moduleCreationStrategy">The module creation strategy.</param>        
        public SimpleCompositionContainer(
            IEnumerable<Assembly> assemblies, 
            IModuleCreationStrategy moduleCreationStrategy)
        {
            _assemblies = assemblies;
            _moduleCreationStrategy = moduleCreationStrategy;            
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        private List<TModule> Modules { get; set; }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        IEnumerable<TModule> ICompositionModulesProvider<TModule>.Modules
        {
            get { return Modules; }
        }

        /// <summary>
        /// Composes the composition modules.
        /// </summary>
        public void Compose()
        {
            Modules = new List<TModule>();
            foreach (var assembly in _assemblies)
            {
                InspectAssembly(assembly);
            }            
        }

        private void InspectAssembly(Assembly assembly)
        {
            try
            {
                var types = assembly.DefinedTypes;
                foreach (var typeInfo in types)
                {
                    if (typeInfo.IsClass && typeInfo.IsAbstract == false
                        && typeInfo.ImplementedInterfaces.Contains(typeof(TModule)))
                    {
                        Modules.Add((TModule)_moduleCreationStrategy.CreateModule(typeInfo.AsType()));
                    }
                }
            }
            //TODO: proper exception handling
            catch (Exception)
            {                
            }            
        }
    }
}
