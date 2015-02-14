using Solid.Practices.IoC;

namespace Solid.Practices.Composition
{
    public interface IModuleRegistrator
    {
        void RegisterModules();
    }

    public class ModuleRegistrator : IModuleRegistrator
    {
        private readonly IIocContainer _iocContainer;
        private readonly ICompositionContainer _compositionContainer;

        public ModuleRegistrator(IIocContainer iocContainer, ICompositionContainer compositionContainer)
        {
            _iocContainer = iocContainer;
            _compositionContainer = compositionContainer;
        }

        public void RegisterModules()
        {
            if (_compositionContainer.Modules != null)
            {
                foreach (var compositionModule in _compositionContainer.Modules)
                {
                    compositionModule.RegisterModule(_iocContainer);
                }                
            }
                
        }
    }
}
