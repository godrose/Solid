using System.Collections.Generic;
using System.IO;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Base class for composition initialization objects
    /// </summary>
    public abstract class CompositionInitializationFacadeBase : ICompositionInitializationFacade
    {
        protected ICompositionContainer CompositionContainer;

        public IAssembliesReadOnlyResolver AssembliesResolver { get; private set; }
        public IEnumerable<ICompositionModule> Modules { get { return CompositionContainer.Modules; } }

        public void Initialize(string rootPath, string[] prefixes = null)
        {
            InitializeComposition(rootPath, prefixes);
            AssembliesResolver = CreateAssembliesResolver();            
        }

        protected abstract IAssembliesReadOnlyResolver CreateAssembliesResolver();

        private void InitializeComposition(string rootPath, string[] prefixes = null)
        {
            if (Directory.Exists(rootPath) == false)
            {
                Directory.CreateDirectory(rootPath);
            }

            CompositionContainer = new CompositionContainer(rootPath, prefixes);
            CompositionContainer.Compose();
        }
    }
}