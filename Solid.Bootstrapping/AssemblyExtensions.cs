using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Bootstrapping
{
    internal static class AssemblyExtensions
    {
        internal static IEnumerable<Assembly> GetAssemblies(this IEnumerable<Assembly> assemblies, AssemblyOptions options)
        {
            options = options ?? new AssemblyOptions();
            if (options.IncludeAll == false)
            {
                if (options.IncludedPrefixes.Length > 0)
                {
                    assemblies = assemblies.Where(t => options.IncludedPrefixes.Any(p => t.FullName.StartsWith(p)));
                }
                if (options.ExcludedPrefixes.Length > 0)
                {
                    assemblies = assemblies.Where(t =>
                        options.ExcludedPrefixes.All(p => t.FullName.StartsWith(p) == false));
                }
            }
            return assemblies;
        }
    }
}