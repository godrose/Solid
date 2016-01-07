using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Linq;
using System.Reflection;
using PCLStorage;
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
        [ImportMany]
        public IEnumerable<TModule> Modules { get; private set; }

        public async void Compose()
        {           
            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(_rootPath);
            //var files = await rootFolder.GetFilesAsync();
            //var matchingFiles = files.Where(t => AllowedModulePatterns.Any(k => t.Path.EndsWith(k)));
            //foreach (var matchingFile in matchingFiles)
            //{
            //    Assembly.Load(new AssemblyName(matchingFile.Name));
            //}            
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
