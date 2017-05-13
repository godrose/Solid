using System;
using System.Collections.Generic;

namespace Solid.Practices.IoC
{
    /// <summary>
    /// Represents Inversion-Of-Control container.
    /// </summary>
    public interface IIocContainer : IIocContainerRegistrator, IIocContainerResolver, IDisposable
    {      
          
    }

    /// <summary>
    /// Allows registering dependencies into the IoC container.
    /// </summary>
    public interface IIocContainerRegistrator
    {
        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        void RegisterTransient<TService>() where TService : class;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>
        void RegisterTransient(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation.</typeparam>
        void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService;

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration.</param>
        /// <param name="implementationType">Type of dependency implementation.</param>        
        void RegisterSingleton(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers an instance of dependency.
        /// </summary>
        /// <typeparam name="TService">Type of dependency.</typeparam>
        /// <param name="instance">Instance of dependency.</param>
        void RegisterInstance<TService>(TService instance) where TService : class;

        /// <summary>
        /// Registers an instance of dependency.
        /// </summary>
        /// <param name="dependencyType">Type of dependency.</param>
        /// <param name="instance">Instance of dependency.</param>
        void RegisterInstance(Type dependencyType, object instance);

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param>
        /// <param name="handler">The handler.</param>
        void RegisterHandler(Type dependencyType, Func<object> handler);

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        void RegisterHandler<TService>(Func<TService> handler) where TService : class;

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencyTypes">The dependency types.</param>
        void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class;

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="dependencies">The dependencies.</param>
        void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class;

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes);

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyType">The dependency type.</param>
        /// <param name="dependencies">The dependencies.</param>
        void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies);
    }

    /// <summary>
    /// Represents object that is capable of resolving services from the IoC container.
    /// </summary>
    public interface IIocContainerResolver
    {
        /// <summary>
        /// Resolves an instance of service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        TService Resolve<TService>() where TService : class;

        /// <summary>
        /// Resolves an instance of service according to the service type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns></returns>
        object Resolve(Type serviceType);
    }

    /// <summary>
    /// Represents means of registering a dependency whose lifetime is bound
    /// to the lifetime of another object.
    /// </summary>
    public interface IIocContainerScoped
    {
        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <param name="lifetimeProvider">The lifetime scope.</param>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        void RegisterScoped(Func<object> lifetimeProvider, Type service, Type implementation);

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        void RegisterScoped<TService, TImplementation>(Func<object> lifetimeProvider);

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        void RegisterScoped<TService>(Func<object> lifetimeProvider);
    }

    /// <summary>
    /// Represents an adapter to an Inversion-Of-Control container.
    /// This is a marker interface.
    /// </summary>
    public interface IIocContainerAdapter
    {
        
    }

    /// <summary>
    /// Represents an adapter to the concrete Inversion-Of-Control container.
    /// This is a marker interface.
    /// </summary>
    /// <typeparam name="TContainer">The type of the concrete Inversion-Of-Control container.</typeparam>
    public interface IIocContainerAdapter<TContainer> : IIocContainerAdapter
    {
        
    }
}
