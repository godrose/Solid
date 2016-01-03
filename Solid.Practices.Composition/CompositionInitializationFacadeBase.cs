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
        /// <summary>
        /// The composition container.
        /// </summary>
        protected ICompositionContainer CompositionContainer;

        /// <summary>
        /// The assemblies resolver.
        /// </summary>
        public IAssembliesReadOnlyResolver AssembliesResolver { get; private set; }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>
        public IEnumerable<ICompositionModule> Modules { get { return CompositionContainer.Modules; } }

        /// <summary>
        /// Initializes composition modules from the provided path.
        /// </summary>
        /// <param name="rootPath">Root path</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates</param>
        public void Initialize(string rootPath, string[] prefixes = null)
        {
            InitializeComposition(rootPath, prefixes);
            AssembliesResolver = CreateAssembliesResolver();            
        }

        /// <summary>
        /// Creates the assemblies resolver.
        /// </summary>
        /// <returns></returns>
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