using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{   
    /// <summary>
    /// Represents strongly-typed composition container
    /// </summary>
    public interface ICompositionContainer : ICompositionContainer<ICompositionModule>, ICompositionModulesProvider
    {        
        
    }

    /// <summary>
    /// Represents composition container which allows composing the composition modules
    /// </summary>
    /// <typeparam name="TModule">Type of composition module</typeparam>
    public interface ICompositionContainer<TModule> : ICompositionModulesProvider<TModule>
    {
        /// <summary>
        /// Composes the composition modules
        /// </summary>
        void Compose();
    }

    /// <summary>
    /// Represents composition container which allows composing the composition modules
    /// while specifying various configuration options.
    /// </summary>
    /// <typeparam name="TModule">The type of composition module.</typeparam>
    public class CompositionContainer<TModule> : ICompositionContainer<TModule>
    {
        private readonly string _rootPath;
        private readonly string[] _prefixes;
        private static readonly string[] AllowedModulePatterns = {"*.dll", "*.exe"};

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

        /// <summary>
        /// Composes the composition modules
        /// </summary>
        public void Compose()
        {
            if (Directory.Exists(_rootPath) == false)
            {
                return;
            }                       
            var directoryCatalogs = GetDirectoryCatalogs();
            var catalog = GetAggregateCatalog(directoryCatalogs);             
            ComposeFromCatalog(catalog);
        }

        private IEnumerable<DirectoryCatalog> GetDirectoryCatalogs()
        {
            return
                AllowedModulePatterns.Select(modulePattern =>
                {
                    return _prefixes == null || _prefixes.Length == 0
                        ? new[] {new DirectoryCatalog(_rootPath, modulePattern)}
                        : _prefixes.Select(k => new DirectoryCatalog(_rootPath, k + modulePattern));
                }).SelectMany(t => t.ToArray());
        }

        private static AggregateCatalog GetAggregateCatalog(IEnumerable<DirectoryCatalog> directoryCatalogs)
        {
            var catalog = new AggregateCatalog();
            foreach (var directoryCatalog in directoryCatalogs)
            {
                catalog.Catalogs.Add(directoryCatalog);
            }
            return catalog;
        }

        private void ComposeFromCatalog(ComposablePartCatalog catalog)
        {
            var container = new System.ComponentModel.Composition.Hosting.CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
                throw;
            }
        }
    }

    /// <summary>
    /// Represents strongly-typed composition container which allows composing the composition modules
    /// while specifying various configuration options
    /// </summary>
    /// <seealso cref="Solid.Practices.Composition.ICompositionContainer{TModule}" />
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
