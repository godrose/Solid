using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Base class for objects that are able to retrieve list of the assemblies to be 
    /// inspected for application elements.
    /// </summary>
    /// <seealso cref="IAssemblySourceProvider" />
    public abstract class AssemblySourceProviderBase : IAssemblySourceProvider
    {
        private readonly string _rootPath;

        protected AssemblySourceProviderBase(string rootPath)
        {
            _rootPath = rootPath;
        }

        private List<Assembly> _inspectedAssemblies;

        public IEnumerable<Assembly> InspectedAssemblies
        {
            get { return _inspectedAssemblies ?? (_inspectedAssemblies = CreateAssemblies()); }
        }

        protected abstract string[] ResolveNamespaces();

        private List<Assembly> CreateAssemblies()
        {
            return SafeAssemblyLoader.LoadAssembliesFromPaths(DiscoverFilePaths()).ToList();
        }

        private IEnumerable<string> DiscoverFilePaths()
        {            
            return DiscoverFilePathsFromNamespaces(ResolveNamespaces());
        }

        private IEnumerable<string> DiscoverFilePathsFromNamespaces(string[] namespaces)
        {
            return AssemblyLoadingManager.Extensions().Select(searchPattern =>
            {
                return namespaces.Length == 0
                    ? Directory.GetFiles(_rootPath, searchPattern)
                    : namespaces.Select(
                        @namespace =>
                            Directory.GetFiles(_rootPath).Select(t => t.ToUpper())
                                .Where(t => t.Contains(@namespace.ToUpper()) && t.EndsWith(searchPattern.ToUpper())))
                        .SelectMany(t => t.ToArray())                        
                        .ToArray();
            }).SelectMany(k => k);
        }
    }          
}
