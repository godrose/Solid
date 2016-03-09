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

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblySourceProviderBase"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        protected AssemblySourceProviderBase(string rootPath)
        {
            _rootPath = rootPath;
        }

        private List<Assembly> _inspectedAssemblies;

        /// <summary>
        /// Gets the assemblies to be inspected.
        /// </summary>
        /// <value>
        /// The assemblies to be inspected.
        /// </value>
        public IEnumerable<Assembly> InspectedAssemblies
        {
            get { return _inspectedAssemblies ?? (_inspectedAssemblies = CreateAssemblies()); }
        }

        /// <summary>
        /// Returns the list of namespaces to be looked for during assembly discovery.
        /// </summary>
        /// <returns></returns>
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
