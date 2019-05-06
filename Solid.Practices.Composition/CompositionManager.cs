using System.Collections.Generic;
using System.Reflection;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows initializing composition from the given path.
    /// </summary>
    public class CompositionManager : ICompositionManager
    {
        ///// <summary>
        ///// Represents <see cref="CompositionManager"/> which uses container
        ///// for creating composition modules.
        ///// </summary>
        ///// <see also cref="Solid.Practices.Composition.CompositionManager" />
        //public class WithIocResolution : CompositionManager
        //{
        //    public WithIocResolution(IIocContainer iocContainer)
        //        : base(new ContainerResolutionStrategy(iocContainer))
        //    {

        //    }
        //}

        private readonly ICompositionModuleCreationStrategy _compositionModuleCreationStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>
        public CompositionManager()
            : this(new ActivatorCreationStrategy())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>
        /// <param name="compositionModuleCreationStrategy">The module creation strategy.</param>
        protected internal CompositionManager(ICompositionModuleCreationStrategy compositionModuleCreationStrategy) =>
            _compositionModuleCreationStrategy = compositionModuleCreationStrategy;

        /// <summary>
        /// The composition container.
        /// </summary>
        protected ICompositionContainer CompositionContainer;

        /// <inheritdoc />
        public IEnumerable<ICompositionModule> Modules => CompositionContainer.Modules;

        /// <inheritdoc />
        public void Initialize(IEnumerable<Assembly> assemblies) => InitializeComposition(assemblies);

        private void InitializeComposition(IEnumerable<Assembly> assemblies)
        {
            CompositionContainer = new CompositionContainer(_compositionModuleCreationStrategy,
                new PreloadedAssemblyLoadingStrategy(assemblies));
            CompositionContainer.Compose();
        }        
    }
}