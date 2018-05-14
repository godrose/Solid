using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Container
{
    /// <summary>
    /// Represents means of extracting type info.
    /// </summary>
    public interface ITypeInfoExtractionService
    {
        /// <summary>
        /// Gets the types that are defined in the assembly.
        /// </summary>
        /// <param name="assembly">The assembly to be queried for types.</param>
        /// <returns></returns>
        IEnumerable<TypeInfo> GetTypes(Assembly assembly);
        /// <summary>
        /// Tests whethere the provided type qualifies as a composition module.
        /// </summary>
        /// <param name="type">The type to be tested.</param>
        /// <param name="moduleType">The composition module type.</param>
        /// <returns>True, if the type qualifies as a composition module; false otherwise.</returns>
        bool IsCompositionModule(TypeInfo type, Type moduleType);
        /// <summary>
        /// Converts the provided <see cref="TypeInfo"/> instance to <see cref="Type"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type ToType(TypeInfo type);
    }

    /// <inheritdoc />    
    public class TypeInfoExtractionService : ITypeInfoExtractionService
    {
        /// <inheritdoc />  
        public IEnumerable<TypeInfo> GetTypes(Assembly assembly) => assembly.DefinedTypes;

        /// <inheritdoc />  
        public bool IsCompositionModule(TypeInfo type, Type moduleType) => type.IsClass && type.IsAbstract == false
                                                                           && type.ImplementedInterfaces.Contains(
                                                                               moduleType);
        /// <inheritdoc />  
        public Type ToType(TypeInfo type) => type.AsType();
    }

    /// <summary>
    /// Represents an exception that is thrown during modules composition.
    /// </summary>
    public class CompositionException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="CompositionException"/>
        /// </summary>
        /// <param name="message">The message.</param>
        public CompositionException(string message)
            :base(message)
        {
            
        }
    }

    public class AggregateAssemblyInspectionException : Exception 
    {
        /// <summary>
        /// The collection modules' inner exceptions.
        /// </summary>
        public Exception[] InnerExceptions { get; }

        /// <summary>
        /// Creates an instance of <see cref="AggregateAssemblyInspectionException"/>
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions.</param>
        public AggregateAssemblyInspectionException(Exception[] innerExceptions)
            : base("Unable to load assemblies")
        {
            InnerExceptions = innerExceptions;
        }
    }    

    /// <summary>
    /// Represents an exception that is thrown during assembly inspection
    /// </summary>
    public class AssemblyInspectionException : Exception
    {
        /// <summary>
        /// The assembly whose inspection resulted in the exception.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Creates an instance of <see cref="AssemblyInspectionException"/>
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="innerException">The inner exception.</param>
        public AssemblyInspectionException(Assembly assembly, Exception innerException)
            :base("Unable to load defined types", innerException)
        {
            Assembly = assembly;
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during modules' creation.
    /// </summary>
    public class AggregateModuleCreationException : Exception
    {
        /// <summary>
        /// The collection modules' inner exceptions.
        /// </summary>
        public ModuleCreationException[] InnerExceptions { get; }

        /// <summary>
        /// Creates an instance of <see cref="AggregateModuleCreationException"/>
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions.</param>
        public AggregateModuleCreationException(ModuleCreationException[] innerExceptions)
            :base("Unable to create composition modules")
        {
            InnerExceptions = innerExceptions;
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during module creation.
    /// </summary>
    public class ModuleCreationException : Exception
    {
        /// <summary>
        /// The module type.
        /// </summary>
        public TypeInfo Type { get; }

        /// <summary>
        /// Creates an instance of <see cref="ModuleCreationException"/>
        /// </summary>
        /// <param name="type">The module type.</param>
        /// <param name="innerException">The inner exception.</param>
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
            catch (Exception ex)
            {                          
                throw new AssemblyInspectionException(assembly, ex);
            }            
        }
    }
}
