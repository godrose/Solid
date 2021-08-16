using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.IoC.Registration
{
    /// <summary>
    /// The extension methods for quick type search.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets all public types from the provided assemblies.
        /// </summary>
        /// <param name="assemblies">The collection of assemblies.</param>
        /// <returns></returns>
        public static Type[] FindAllTypes(this IEnumerable<Assembly> assemblies)
        {
            return FindAllTypesImpl(assemblies);
        }

        private static Type[] FindAllTypesImpl(IEnumerable<Assembly> assemblies)
        {
            var allTypes = assemblies
                .SelectMany(assembly => assembly.ExportedTypes)
                .ToArray();

            return allTypes;
        }

        /// <summary>
        /// Finds all types that match the specified ending in the provided assemblies.
        /// </summary>
        /// <param name="assemblies">The collection of assemblies.</param>
        /// <param name="ending">The specified ending.</param>
        /// <returns></returns>
        public static Type[] FindTypesByEnding(this IEnumerable<Assembly> assemblies, string ending)
        {
            var allTypes = FindAllTypesImpl(assemblies);
            var matches = allTypes
                .Where(type => type.Name.EndsWith(ending));
            return matches.ToArray();
        }

        /// <summary>
        /// Finds all types that match the specified contract in the provided assemblies.
        /// </summary>
        /// <param name="assemblies">The collection of assemblies.</param>
        /// <param name="contractType">The type of the specified contract.</param>
        /// <returns></returns>
        public static Type[] FindTypesByContract(this IEnumerable<Assembly> assemblies, Type contractType)
        {
            var allTypes = FindAllTypesImpl(assemblies);
            var typeMatchPredicate = contractType.IsInterface
                ? type => type.GetImplementedInterfaces().Contains(contractType)
                : (Func<Type, bool>) (type => type.IsClass && type.IsSubclassOf(contractType)); 
            var matches = allTypes.Where(typeMatchPredicate);
            return matches.ToArray();
        }
    }
}