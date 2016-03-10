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
#if NET
                        return Assembly.LoadFrom(k);
#endif
#if NETFX_CORE || WINDOWS_UWP
                    //must implement loading assembly from path
                                        
                    return (Assembly)null;
#endif

                }
                catch (Exception)
                {

                    return null;
                }
            }).Where(k => k != null);
        }
    }
}
