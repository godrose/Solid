using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    static class SafeAssemblyLoader
    {
        internal static IEnumerable<Assembly> LoadAssembliesFromPaths(IEnumerable<string> paths)
        {
            return paths.Select(k =>
            {
                try
                {
                    return Assembly.LoadFrom(k);
                }
                catch (Exception)
                {

                    return null;
                }
            }).Where(k => k != null);
        }
    }
}
