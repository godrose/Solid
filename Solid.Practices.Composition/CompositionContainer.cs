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
    public interface ICompositionModulesProvider : ICompositionModulesProvider<ICompositionModule>
    {        
    }

    public interface ICompositionModulesProvider<TModule>
    {
        IEnumerable<TModule> Modules { get; }
    }

    public interface ICompositionContainer : ICompositionContainer<ICompositionModule>, ICompositionModulesProvider
    {        
        
    }

    public interface ICompositionContainer<TModule> : ICompositionModulesProvider<TModule>
    {
        void Compose();
    }

    public class CompositionContainer<TModule> : ICompositionContainer<TModule>
    {
        private readonly string _rootPath;
        private static readonly string[] AllowedModulePatterns = {"*.dll", "*.exe"};

        public CompositionContainer(string rootPath)
        {
            _rootPath = rootPath;
        }

        [ImportMany]
        public IEnumerable<TModule> Modules { get; private set; }

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
            return AllowedModulePatterns.Select(modulePattern => new DirectoryCatalog(_rootPath, modulePattern));
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

    public class CompositionContainer : CompositionContainer<ICompositionModule>, ICompositionContainer
    {
        public CompositionContainer(string rootPath) 
            : base(rootPath)
        {
        }
    }
}
