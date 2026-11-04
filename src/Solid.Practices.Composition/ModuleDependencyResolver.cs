using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Resolves the dependencies of the modules that were loaded into the default context
    /// but are unknown to the host's dependency manifest, from the modules' folders.
    /// </summary>
    internal static class ModuleDependencyResolver
    {
        private static readonly ConcurrentDictionary<string, byte> Folders = new ConcurrentDictionary<string, byte>();

        private static int _isSubscribed;

        internal static void Register(string modulePath)
        {
            Folders.TryAdd(Path.GetDirectoryName(Path.GetFullPath(modulePath)), 0);
            if (Interlocked.Exchange(ref _isSubscribed, 1) == 0)
            {
                AssemblyLoadContext.Default.Resolving += OnResolving;
            }
        }

        private static Assembly OnResolving(AssemblyLoadContext context, AssemblyName name)
        {
            foreach (var folder in Folders.Keys)
            {
                var path = Path.Combine(folder, name.Name + ".dll");
                if (File.Exists(path))
                {
                    return context.LoadFromAssemblyPath(path);
                }
            }
            return null;
        }
    }
}
