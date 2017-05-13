using System;
using System.Collections.Generic;
using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Represents a module, i.e. an encapsulated decoupled
    /// piece of application that is not referenced explicitly
    /// and gets discovered in the run time instead.
    /// </summary>
    public interface IModule
    {
        
    }

    /// <summary>
    /// Represents a composition module, i.e. a module which 
    /// can be discovered and instantiated during the application composition.
    /// It is a marker interface.    
    /// </summary>
    public interface ICompositionModule
    {
        
    }

    /// <summary>
    /// Represents a composition module, which may register dependencies into ioc container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of ioc container.</typeparam>
    public interface ICompositionModule<in TIocContainer> : ICompositionModule        
    {
        /// <summary>
        /// Registers dependencies into the ioc container.
        /// </summary>
        /// <param name="iocContainer">The ioc container.</param>
        void RegisterModule(TIocContainer iocContainer);
    }    

    /// <summary>
    /// Represents a composition module which contains logic that is
    /// executed upon registration.
    /// </summary>
    public interface IPlainCompositionModule : ICompositionModule
    {
        /// <summary>
        /// Registers the composition module.
        /// </summary>
        void RegisterModule();
    }

    /// <summary>
    /// Represents a composition module, which may register dependencies
    /// that are dependent on the lifetime of another object.
    /// </summary>
    public interface IScopedCompositionModule : ICompositionModule
    {
        /// <summary>
        /// Registers the dependencies into the ioc container.
        /// </summary>
        /// <param name="iocContainer">The ioc container.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        void RegisterModule(IIocContainerScoped iocContainer, Func<object> lifetimeScopeProvider);
    }

    /// <summary>
    /// Represents a composition module, which is able to
    /// register collections of other modules into the ioc container.
    /// </summary>
    /// <seealso cref="ICompositionModule" />
    public interface IHierarchicalCompositionModule<in TIocContainer> : ICompositionModule        
    {
        /// <summary>
        /// Registers the modules into the ioc container.
        /// </summary>
        /// <param name="iocContainer">The ioc container.</param>
        /// <param name="modules">The modules.</param>
        void RegisterModules(TIocContainer iocContainer, IEnumerable<ICompositionModule> modules);
    }    
}