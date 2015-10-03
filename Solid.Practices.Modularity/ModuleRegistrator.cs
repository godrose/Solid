using System.Collections.Generic;
using System.Linq;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Allows registering composition modules into IoC container
    /// </summary>
    public interface IModuleRegistrator : IMiddleware
    {
        /// <summary>
        /// Registers composition modules into IoC container
        /// </summary>
        /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
        /// <param name="iocContainer">IoC container</param>
        void RegisterModules<TIocContainer>(TIocContainer iocContainer) where TIocContainer : IIocContainer;
    }

    public class ModuleRegistrator : IModuleRegistrator
    {
        private readonly IEnumerable<ICompositionModule> _modules;        

        public ModuleRegistrator(IEnumerable<ICompositionModule> modules)
        {
            _modules = modules;            
        }        

        public void RegisterModules<TIocContainer>(TIocContainer iocContainer) where TIocContainer : IIocContainer
        {
            RegisterModulesInternal(iocContainer);
        }        

        TIocContainer IMiddleware.Apply<TIocContainer>(TIocContainer iocContainer)
        {
            RegisterModulesInternal(iocContainer);
            return iocContainer;
        }

        private void RegisterModulesInternal<TIocContainer>(TIocContainer iocContainer) where TIocContainer : IIocContainer
        {
            if (_modules != null)
            {
                foreach (var compositionModule in _modules.OfType<ICompositionModule<TIocContainer>>())
                {
                    compositionModule.RegisterModule(iocContainer);
                }
            }
        }
    }
}
