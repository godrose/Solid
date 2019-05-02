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
        private readonly ITypeInfoExtractionService _typeInfoExtractionService;
        private readonly ICompositionModuleCreationStrategy _compositionModuleCreationStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="typeInfoExtractionService">The type info extraction service.</param>
        /// <param name="compositionModuleCreationStrategy">The module creation strategy.</param>        
        public SimpleCompositionContainer(
            IEnumerable<Assembly> assemblies, 
            ITypeInfoExtractionService typeInfoExtractionService,
            ICompositionModuleCreationStrategy compositionModuleCreationStrategy)
        {
            _assemblies = assemblies;
            _typeInfoExtractionService = typeInfoExtractionService;
            _compositionModuleCreationStrategy = compositionModuleCreationStrategy;            
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        private List<TModule> Modules { get; set; }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        IEnumerable<TModule> ICompositionModulesProvider<TModule>.Modules => Modules;

        /// <summary>
        /// Composes the composition modules.
        /// </summary>
        public void Compose()
        {
            Modules = new List<TModule>();
            var innerExceptions = new List<Exception>();

            try
            {
                foreach (var assembly in _assemblies)
                {
                    try
                    {
                        InspectAssembly(assembly);
                    }
                    catch (Exception e)
                    {
                        innerExceptions.Add(e);
                    }
                }               
            }            
            catch (Exception e)
            {
                innerExceptions.Add(e);               
            }
            if (innerExceptions.Count > 0)
            {
                throw new AggregateAssemblyInspectionException(innerExceptions.ToArray());
            }
        }

        private void InspectAssembly(Assembly assembly)
        {
            try
            {
                var innerExceptions = new List<ModuleCreationException>();
                var types = _typeInfoExtractionService.GetTypes(assembly);
                foreach (var typeInfo in types)
                {
                    try
                    {
                        if (_typeInfoExtractionService.IsCompositionModule(typeInfo, typeof(TModule)))
                        {
                            Modules.Add(
                                (TModule) _compositionModuleCreationStrategy.CreateCompositionModule(
                                    typeInfo.AsType()));
                        }
                    }
                    catch (Exception e)
                    {
                        innerExceptions.Add(new ModuleCreationException(typeInfo, e));
                    }
                }
                if (innerExceptions.Count > 0)
                {
                    throw new AggregateModuleCreationException(innerExceptions.ToArray());
                }
            }
            catch (AggregateModuleCreationException)
            {
                throw;
            }            
            catch (Exception ex)
            {                          
                throw new AssemblyInspectionException(assembly.FullName, ex);
            }            
        }
    }
}
