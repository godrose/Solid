using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    public interface ICompositionModulesProvider
    {
        IEnumerable<ICompositionModule> Modules { get; }
    }

    public interface ICompositionContainer : ICompositionModulesProvider
    {        
        void Compose();
    }

    public class CompositionContainer : ICompositionContainer
    {
        private readonly string _rootPath;
        private static readonly string[] AllowedModulePatterns = new[] {"*.dll", "*.exe"};

        public CompositionContainer(string rootPath)
        {
            _rootPath = rootPath;
        }

        [ImportMany(typeof(ICompositionModule))]
        public IEnumerable<ICompositionModule> Modules { get; private set; }

        public void Compose()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            if (Directory.Exists(_rootPath) == false)
            {
                return;
            }
            foreach (var modulePattern in AllowedModulePatterns)
            {
                DirectoryCatalog dllCatalog = new DirectoryCatalog(_rootPath, modulePattern);
                catalog.Catalogs.Add(dllCatalog);   
            }            
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
}
