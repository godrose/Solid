using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using McMaster.NETCore.Plugins;

namespace Solid.Practices.Composition.Tests.Steps.Adapters
{
    internal static class DynamicLoader
    {
        public static IEnumerable<Assembly> LoadAssemblies(IEnumerable<string> paths)
        {
            return paths.Select(path =>
                PluginLoader.CreateFromAssemblyFile(assemblyFile: Path.Combine(Directory.GetCurrentDirectory(), path), 
                    t => t.PreferSharedTypes = true
                ).LoadDefaultAssembly());
        }
    }
}