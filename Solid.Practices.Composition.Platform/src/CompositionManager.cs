using System.Collections.Generic;
using System.IO;
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
        /// <param name="modulesPath">Root path</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates</param>
        public void Initialize(string modulesPath, string[] prefixes = null)
        {
            InitializeComposition(modulesPath, prefixes);            
        }       

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