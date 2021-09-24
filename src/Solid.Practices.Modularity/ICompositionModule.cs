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
    /// Represents a composition module, which may register dependencies into an IoC container.
    /// </summary>
    /// <typeparam name="TDependencyRegistrator">The type of the dependency registrator.</typeparam>
    public interface ICompositionModule<in TDependencyRegistrator> : ICompositionModule        
    {
        /// <summary>
        /// Registers dependencies.
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        void RegisterModule(TDependencyRegistrator dependencyRegistrator);
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
        /// Registers the dependencies.
        /// </summary>
        /// <param name="dependencyRegistratorScoped">The scoped dependency registrator.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        void RegisterModule(IDependencyRegistratorScoped dependencyRegistratorScoped, Func<object> lifetimeScopeProvider);
    }

    /// <summary>
    /// Represents a composition module, which is able to
    /// register collections of other modules into the IoC container.
    /// </summary>
    /// <seealso cref="ICompositionModule" />
    public interface IHierarchicalCompositionModule<in TDependencyRegistrator> : ICompositionModule        
    {
        /// <summary>
        /// Registers the modules.
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="modules">The modules collection.</param>
        void RegisterModules(TDependencyRegistrator dependencyRegistrator, IEnumerable<ICompositionModule> modules);
    }    
}