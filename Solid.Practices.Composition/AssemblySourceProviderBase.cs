using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly string[] _prefixes;
        private IAssemblyLoadingStrategy _assemblyLoadingStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblySourceProviderBase"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="prefixes">The prefixes.</param>
        protected AssemblySourceProviderBase(string rootPath, string[] prefixes = null)
        {
            _rootPath = rootPath;
            _prefixes = prefixes;
        }

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

        private Assembly[] CreateAssemblies()
        {
            _assemblyLoadingStrategy = new FileSystemBasedAssemblyLoadingStrategy(_rootPath, _prefixes,
                ResolveNamespaces(), AssemblyLoadingManager.Extensions().ToArray());
            return _assemblyLoadingStrategy.Load().ToArray();            
        }
    }
}
