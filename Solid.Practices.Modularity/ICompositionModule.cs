using System;
using System.Collections.Generic;
using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Represents a composistion module.
    /// It is a marker interface.
    /// </summary>
    public interface ICompositionModule
    {
        
    }

    /// <summary>
    /// Represents a composition module, which may be registered into IoC container
    /// </summary>
    /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
    public interface ICompositionModule<in TIocContainer> : ICompositionModule where TIocContainer : IIocContainer
    {
        /// <summary>
        /// Registers composition module into IoC container
        /// </summary>
        /// <param name="iocContainer">IoC container</param>
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
    /// Represents a composition module, which  may register dependencies
    /// that are dependent on the lifetime of another object.
    /// </summary>
    public interface IScopedCompositionModule : ICompositionModule
    {
        /// <summary>
        /// Registers the composition module into the ioc container
        /// </summary>
        /// <param name="container">The simple container.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        void RegisterModule(IIocContainerScoped container, Func<object> lifetimeScopeProvider);
    }

    /// <summary>
    /// Represents a composition module, which is able to
    /// register collections of other modules into the container.
    /// </summary>
    /// <seealso cref="ICompositionModule" />
    public interface IHierarchicalCompositionModule : ICompositionModule
    {
        /// <summary>
        /// Registers the modules into the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="modules">The modules.</param>
        void RegisterModules(IIocContainer container, IEnumerable<ICompositionModule> modules);
    }
}