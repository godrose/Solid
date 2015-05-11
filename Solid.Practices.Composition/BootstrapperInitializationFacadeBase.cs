using System.Collections.Generic;
using System.IO;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    public abstract class BootstrapperInitializationFacadeBase<TIocContainer> : IBootstrapperInitializationFacade where TIocContainer : IIocContainer
    {
        private readonly TIocContainer _iocContainer;
        protected ICompositionContainer CompositionContainer;

        protected BootstrapperInitializationFacadeBase(TIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public IAssembliesReadOnlyResolver AssembliesResolver { get; private set; }
        public IEnumerable<ICompositionModule> Modules { get { return CompositionContainer.Modules; } }

        public void Initialize(string rootPath)
        {
            InitializeComposition(rootPath);
            AssembliesResolver = CreateAssembliesResolver();
            RegisterModules();
        }

        protected abstract IAssembliesReadOnlyResolver CreateAssembliesResolver();

        private void InitializeComposition(string rootPath)
        {
            if (Directory.Exists(rootPath) == false)
            {
                Directory.CreateDirectory(rootPath);
            }

            CompositionContainer = new CompositionContainer(rootPath);
            CompositionContainer.Compose();
        }

        private void RegisterModules()
        {
            var moduleRegistrator = new ModuleRegistrator<TIocContainer>(_iocContainer, CompositionContainer);
            moduleRegistrator.RegisterModules();
        }
    }
}