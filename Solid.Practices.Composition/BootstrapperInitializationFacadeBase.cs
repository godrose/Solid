using System.IO;
using Solid.Practices.IoC;

namespace Solid.Practices.Composition
{
    public abstract class BootstrapperInitializationFacadeBase : IBootstrapperInitializationFacade
    {
        private readonly IIocContainer _iocContainer;
        protected ICompositionContainer CompositionContainer;

        protected BootstrapperInitializationFacadeBase(IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public IAssembliesReadOnlyResolver AssembliesResolver { get; private set; }

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
            var moduleRegistrator = new ModuleRegistrator(_iocContainer, CompositionContainer);
            moduleRegistrator.RegisterModules();
        }
    }
}