using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.IoC.Registration
{
    public static class AssemblyExtensions
    {
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

        public static Type[] FindTypesByName(this IEnumerable<Assembly> assemblies, string ending)
        {
            var allTypes = FindAllTypesImpl(assemblies);
            var matches = allTypes
                .Where(type => type.Name.EndsWith(ending));
            return matches.ToArray();
        }

        public static Type[] FindTypesByContract(this IEnumerable<Assembly> assemblies, Type contractType)
        {
            var allTypes = FindAllTypesImpl(assemblies);
            var matches = allTypes
                .Where(type => type.GetImplementedInterfaces().Contains(contractType));
            return matches.ToArray();
        }
    }
}