using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The assembly loader.
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// Loads assemblies from their paths. Replace to have custom way of loading the assemblies.
        /// </summary>
        public static Func<IEnumerable<string>, IEnumerable<Assembly>> LoadAssembliesFromPaths { get; set; } = paths => paths.Select(k =>
        {
            try
            {
                var name = Path.GetFileNameWithoutExtension(k);
                return name == null ? null : Assembly.Load(new AssemblyName(name));
            }
            catch (Exception)
            {
                return null;
            }
        }).Where(k => k != null);
    }
}
