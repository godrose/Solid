using System.Collections.Generic;
using System.IO;
using System.Linq;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents composition container which allows composing the composition modules
    /// while specifying various configuration options.
    /// </summary>
    /// <typeparam name="TModule">The type of composition module.</typeparam>
    public class CompositionContainer<TModule> : ICompositionContainer<TModule>
    {
        private readonly string _rootPath;
        private readonly string[] _prefixes;
        private static readonly string[] AllowedModulePatterns = { "*.dll", "*.exe" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="prefixes">The prefixes.</param>
        public CompositionContainer(string rootPath, string[] prefixes = null)
        {
            _rootPath = rootPath;
            _prefixes = prefixes;
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>        
        public IEnumerable<TModule> Modules { get; private set; }

        void ICompositionContainer<TModule>.Compose()
        {
            var assemblies = SafeAssemblyLoader.LoadAssembliesFromNames(DiscoverAssemblyNames());            
            ICompositionContainer<TModule> innerContainer = new PortableCompositionContainer<TModule>(assemblies);
            //throws ex
            innerContainer.Compose();
            Modules = innerContainer.Modules;
        }

        private IEnumerable<string> DiscoverAssemblyNames()
        {
            return DiscoverFilePaths().Select(Path.GetFileNameWithoutExtension);
        }

        private IEnumerable<string> DiscoverFilePaths()
        {
            return AllowedModulePatterns.Select(searchPattern =>
            {
                return _prefixes == null || _prefixes.Length == 0
                    ? Directory.GetFiles(_rootPath, searchPattern)
                    : _prefixes.Select(prefix => Directory.GetFiles(_rootPath, prefix + searchPattern))
                        .SelectMany(t => t)
                        .ToArray();
            }).SelectMany(k => k);
        }
    }

    /// <summary>
    /// Represents strongly-typed composition container which allows composing the composition modules
    /// while specifying various configuration options
    /// </summary>
    /// <seealso cref="Composition.ICompositionContainer{TModule}" />
    public class CompositionContainer : CompositionContainer<ICompositionModule>, ICompositionContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionContainer"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="prefixes">The prefixes.</param>
        public CompositionContainer(string rootPath, string[] prefixes = null)
            : base(rootPath, prefixes)
        {
        }
    }
}
