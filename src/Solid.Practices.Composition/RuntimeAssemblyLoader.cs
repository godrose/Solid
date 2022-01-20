using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Solid.Practices.Composition
{
    public static class RuntimeAssemblyLoader
    {
        public static IEnumerable<Assembly> Get(IEnumerable<string> files)
        {
            return files.Select(r =>
                AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), r))).ToArray();
        }
    }
}

