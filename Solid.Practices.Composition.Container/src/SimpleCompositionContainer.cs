using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Container
{
    public interface ITypeInfoExtractionService
    {
        IEnumerable<TypeInfo> GetTypes(Assembly assembly);
        bool IsCompositionModule(TypeInfo type, Type moduleType);
        Type ToType(TypeInfo type);
    }

    public class TypeInfoExtractionService : ITypeInfoExtractionService
    {
        public IEnumerable<TypeInfo> GetTypes(Assembly assembly) => assembly.DefinedTypes;

        public bool IsCompositionModule(TypeInfo type, Type moduleType) => type.IsClass && type.IsAbstract == false
                                                                           && type.ImplementedInterfaces.Contains(
                                                                               moduleType);

        public Type ToType(TypeInfo type) => type.AsType();
    }

    public class CompositionException : Exception
    {
        public CompositionException(string message)
            :base(message)
        {
            
        }
    }

    public class AssemblyInspectionException : Exception
    {
        public Assembly Assembly { get; }

        public AssemblyInspectionException(Assembly assembly, Exception innerException)
            :base("Unable to load defined types", innerException)
        {
            Assembly = assembly;
        }
    }

    public class AggregateModuleCreationException : Exception
    {
        public ModuleCreationException[] InnerExceptions { get; }

        public AggregateModuleCreationException(ModuleCreationException[] innerExceptions)
            :base("Unable to create composition modules")
        {
            InnerExceptions = innerExceptions;
        }
    }

    public class ModuleCreationException : Exception
    {
        public TypeInfo Type { get; }

        public ModuleCreationException(TypeInfo type, Exception innerException)
            :base("Unable to create module for the specified type", innerException)
        {
            Type = type;
        }
    }

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
                                    _typeInfoExtractionService.ToType(typeInfo)));
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
            //TODO: proper exception handling
            catch (Exception ex)
            {          
                
                throw new AssemblyInspectionException(assembly, ex);
            }            

        }
    }
}
