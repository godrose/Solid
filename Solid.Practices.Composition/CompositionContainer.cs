using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

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
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(_rootPath);
            catalog.Catalogs.Add(directoryCatalog);
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
