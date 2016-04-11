using System.Collections.Generic;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows initializing composition from the given path.
    /// </summary>
    public class CompositionManager : ICompositionManager
    {
        /// <summary>
        /// The composition container.
        /// </summary>
        protected ICompositionContainer CompositionContainer;        

        /// <summary>
        /// Collection of composition modules.
        /// </summary>
        public IEnumerable<ICompositionModule> Modules { get { return CompositionContainer.Modules; } }

        /// <summary>
        /// Initializes composition modules from the provided path.
        /// </summary>
        /// <param name="rootPath">Root path.</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates.</param>
        public void Initialize(string rootPath, string[] prefixes = null)
        {
            InitializeComposition(rootPath, prefixes);            
        }       

        private void InitializeComposition(string rootPath, string[] prefixes = null)
        {            
            CompositionContainer = new CompositionContainer(rootPath, prefixes);
            CompositionContainer.Compose();
        }
    }
}