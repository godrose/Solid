using System.Collections.Generic;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows initializing composition from the given path.
    /// </summary>
    public class CompositionManager : ICompositionManager
    {
        /// <summary>
        /// Represents <see cref="CompositionManager"/> which uses container
        /// for creating composition modules.
        /// </summary>
        /// <seealso cref="Solid.Practices.Composition.CompositionManager" />
        public class WithIocResolution : CompositionManager
        {
            public WithIocResolution(IIocContainer iocContainer)
                :base(new ContainerResolutionStrategy(iocContainer))
            {
                
            }
        }

        private readonly IModuleCreationStrategy _moduleCreationStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>
        public CompositionManager()
            :this(new ActivatorCreationStrategy())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>
        /// <param name="moduleCreationStrategy">The module creation strategy.</param>
        protected internal CompositionManager(IModuleCreationStrategy moduleCreationStrategy)
        {
            _moduleCreationStrategy = moduleCreationStrategy;
        }

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
            CompositionContainer = new CompositionContainer(_moduleCreationStrategy, rootPath, prefixes);
            CompositionContainer.Compose();
        }
    }
}