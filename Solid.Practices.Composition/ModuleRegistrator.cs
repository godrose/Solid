using System.Linq;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    public interface IModuleRegistrator
    {
        void RegisterModules();
    }

    public class ModuleRegistrator<TIocContainer> : IModuleRegistrator where TIocContainer : IIocContainer
    {
        private readonly TIocContainer _iocContainer;
        private readonly ICompositionContainer _compositionContainer;

        public ModuleRegistrator(TIocContainer iocContainer, ICompositionContainer compositionContainer)
        {
            _iocContainer = iocContainer;
            _compositionContainer = compositionContainer;
        }

        public void RegisterModules()
        {
            if (_compositionContainer.Modules != null)
            {
                foreach (var compositionModule in _compositionContainer.Modules.OfType<ICompositionModule<TIocContainer>>())
                {
                    compositionModule.RegisterModule(_iocContainer);
                }                
            }                
        }
    }
}
