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

    /// <summary>
    /// Enabled to register collection of <see cref="ICompositionModule"/> into the provided <see cref="IIocContainer"/>>
    /// both explicitly and via <see cref="IMiddleware"/>
    /// </summary>
    /// <seealso cref="IModuleRegistrator" />
    public class ModuleRegistrator : IModuleRegistrator
    {
        private readonly IEnumerable<ICompositionModule> _modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRegistrator"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public ModuleRegistrator(IEnumerable<ICompositionModule> modules)
        {
            _modules = modules;            
        }

        /// <summary>
        /// Registers composition modules into IoC container
        /// </summary>
        /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
        /// <param name="iocContainer">IoC container</param>
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
