using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The runtime assembly loader.
    /// </summary>
    public static class RuntimeAssemblyLoader
    {
        /// <summary>
        /// Returns the list os assemblies loaded from the respective files.
        /// </summary>
        /// <param name="files">The collection of files.</param>
        /// <returns>The collection of assemblies.</returns>
        public static IEnumerable<Assembly> Get(IEnumerable<string> files)
        {
            return files.Select(r =>
                AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), r))).ToArray();
        }
    }
}

