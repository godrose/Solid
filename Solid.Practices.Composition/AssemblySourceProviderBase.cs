using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Practices.Composition.Contracts;

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
        protected AssemblySourceProviderBase(string rootPath) => _rootPath = rootPath;

        private Assembly[] _inspectedAssemblies;

        /// <summary>
        /// Gets the assemblies to be inspected.
        /// </summary>
        /// <value>
        /// The assemblies to be inspected.
        /// </value>
        public IEnumerable<Assembly> Assemblies => _inspectedAssemblies ?? (_inspectedAssemblies = CreateAssemblies());

        /// <summary>
        /// Returns the list of namespaces to be looked for during assembly discovery.
        /// </summary>
        /// <returns></returns>
        protected abstract string[] ResolveNamespaces();

        private Assembly[] CreateAssemblies() =>
            SafeAssemblyLoader.LoadAssembliesFromNames(DiscoverAssemblyNames()).ToArray();

        private IEnumerable<string> DiscoverAssemblyNames() => DiscoverFilePathsFromNamespaces(ResolveNamespaces())
            .Select(Path.GetFileNameWithoutExtension);

        private IEnumerable<string> DiscoverFilePathsFromNamespaces(string[] namespaces) => AssemblyLoadingManager
            .Extensions().Select(searchPattern => namespaces.Length == 0
                ? PlatformProvider.Current.GetFiles(_rootPath, searchPattern)
                : namespaces.Select(
                        @namespace =>
                            PlatformProvider.Current.GetFiles(_rootPath).Select(t => t.ToUpper())
                                .Where(t => t.Contains(@namespace.ToUpper()) && t.EndsWith(searchPattern.ToUpper())))
                    .SelectMany(t => t.ToArray())
                    .ToArray()).SelectMany(k => k);
    }
}
