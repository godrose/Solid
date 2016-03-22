using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Allows registering composition modules into IoC container.
    /// </summary>
    /// <typeparam name="TIocContainerConstraint">Type of IoC container constraint.</typeparam>
    public interface IModuleRegistrator<in TIocContainerConstraint>
    {
        /// <summary>
        /// Registers composition modules into IoC container.
        /// </summary>
        /// <typeparam name="TIocContainer">Type of IoC container.</typeparam>
        /// <param name="iocContainer">IoC container.</param>
        void RegisterModules<TIocContainer>(TIocContainer iocContainer) where TIocContainer : TIocContainerConstraint;
    }

    /// <summary>
    /// Allows registering composition modules into IoC container.
    /// </summary>
    public interface IModuleRegistrator : IModuleRegistrator<IIocContainer>, IMiddleware
    {        
    }

    /// <summary>
    /// Allows registering composition modules into IoC container using lifetime scope provider.
    /// </summary>
    public interface IScopedModuleRegistrator : IModuleRegistrator<IIocContainerScoped>
    {       
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

                foreach (var plainCompositionModule in _modules.OfType<IPlainCompositionModule>())
                {
                    plainCompositionModule.RegisterModule();
                }                
            }
        }        
    }

    /// <summary>
    /// Allows registering composition modules into IoC container using lifetime scope provider.
    /// </summary>
    public class ScopedModuleRegistrator : IScopedModuleRegistrator
    {       
        private readonly IEnumerable<ICompositionModule> _modules;
        private readonly Func<object> _lifetimeScopeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRegistrator"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        public ScopedModuleRegistrator(
            IEnumerable<ICompositionModule> modules, 
            Func<object> lifetimeScopeProvider)
        {
            _modules = modules;
            _lifetimeScopeProvider = lifetimeScopeProvider;
        }

        /// <summary>
        /// Registers composition modules into IoC container
        /// </summary>
        /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
        /// <param name="iocContainer">IoC container</param>
        
        public void RegisterModules<TIocContainer>(TIocContainer iocContainer) 
            where TIocContainer : IIocContainerScoped
        {
            RegisterModulesInternal(iocContainer, _lifetimeScopeProvider);
        }       

        private void RegisterModulesInternal<TIocContainer>(TIocContainer iocContainer, Func<object> lifetimeScopeProvider)
            where TIocContainer : IIocContainerScoped
        {
            if (_modules != null)
            {
                foreach (var scopedModule in _modules.OfType<IScopedCompositionModule>())
                {
                    scopedModule.RegisterModule(iocContainer, lifetimeScopeProvider);
                }
            }
        }
    }
}
