using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    static class SafeAssemblyLoader
    {
        internal static IEnumerable<Assembly> LoadAssembliesFromNames(IEnumerable<string> names)
        {
            return names.Select(k =>
            {
                try
                {
                    return Assembly.Load(new AssemblyName(k));
                }
                catch (Exception)
                {

                    return null;
                }
            }).Where(k => k != null);
        }
    }
}
