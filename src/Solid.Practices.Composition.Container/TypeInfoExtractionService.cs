using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        /// Tests whether the provided type qualifies as a composition module.
        /// </summary>
        /// <param name="type">The type to be tested.</param>
        /// <param name="moduleType">The composition module type.</param>
        /// <returns>True, if the type qualifies as a composition module; false otherwise.</returns>
        bool IsCompositionModule(TypeInfo type, Type moduleType);
    }

    /// <inheritdoc />    
    public class TypeInfoExtractionService : ITypeInfoExtractionService
    {
        /// <inheritdoc />  
        public IEnumerable<TypeInfo> GetTypes(Assembly assembly) => assembly.DefinedTypes;

        /// <inheritdoc />  
        public bool IsCompositionModule(TypeInfo type, Type moduleType) => type.IsClass && type.IsAbstract == false
                                                                                        && type.ImplementedInterfaces
                                                                                            .Contains(
                                                                                                moduleType);
    }
}