using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    public static class AssemblyLoader
    {
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
