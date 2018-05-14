using System.Collections.Generic;
using Solid.Practices.Composition.Container;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{    
    /// <summary>
    /// Represents composition container which allows composing the composition modules
    /// while specifying various configuration options.
    /// </summary>
    /// <typeparam name="TModule">The type of composition module.</typeparam>
    public class CompositionContainer<TModule> : ICompositionContainer<TModule> where TModule : ICompositionModule
    {
        private readonly ICompositionModuleCreationStrategy _compositionModuleCreationStrategy;
        private readonly IAssemblyLoadingStrategy _assemblyLoadingStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="compositionModuleCreationStrategy">The module creation strategy.</param>
        /// <param name="assemblyLoadingStrategy">The assembly loading strategy.</param>       
        public CompositionContainer(
            ICompositionModuleCreationStrategy compositionModuleCreationStrategy, 
            IAssemblyLoadingStrategy assemblyLoadingStrategy)
        {
            _compositionModuleCreationStrategy = compositionModuleCreationStrategy;
            _assemblyLoadingStrategy = assemblyLoadingStrategy;          
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>        
        public IEnumerable<TModule> Modules { get; private set; }

        void ICompositionContainer<TModule>.Compose()
        {
            var assemblies = _assemblyLoadingStrategy.Load();

            ICompositionContainer<TModule> innerContainer = new SimpleCompositionContainer<TModule>(
                assemblies,
                //TODO: Consider replacing with dependency injection for improved testability
                new TypeInfoExtractionService(), 
                _compositionModuleCreationStrategy);
            innerContainer.Compose();
            Modules = innerContainer.Modules;
        }        
    }

    /// <summary>
    /// Represents strongly-typed composition container which allows composing the composition modules
    /// while specifying various configuration options
    /// </summary>
    /// <seealso cref="ICompositionContainer" />
    public class CompositionContainer : CompositionContainer<ICompositionModule>, ICompositionContainer
    {
        /// <inheritdoc />      
        public CompositionContainer(
            ICompositionModuleCreationStrategy compositionModuleCreationStrategy, 
            IAssemblyLoadingStrategy assemblyLoadingStrategy)
            : base(compositionModuleCreationStrategy, assemblyLoadingStrategy)
        {
        }
    }
}
