using System;

namespace Solid.Practices.IoC
{
    /// <summary>
    /// Represents Inversion-Of-Control container
    /// </summary>
    public interface IIocContainer : IIocContainerRegistrator, IIocContainerResolver, IDisposable
    {      
          
    }

    /// <summary>
    /// Allows registering dependencies into the IoC container
    /// </summary>
    public interface IIocContainerRegistrator
    {
        /// <summary>
        /// Registers dependency in a transient lifetime style
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation</typeparam>
        void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService;

        /// <summary>
        /// Registers dependency in a transient lifetime style
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        void RegisterTransient<TService>() where TService : class;

        /// <summary>
        /// Registers dependency in a transient lifetime style
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration</param>
        /// <param name="implementationType">Type of dependency implementation</param>
        void RegisterTransient(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers dependency as a singleton
        /// </summary>
        /// <typeparam name="TService">Type of dependency declaration</typeparam>
        /// <typeparam name="TImplementation">Type of dependency implementation</typeparam>
        void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService;

        /// <summary>
        /// Registers dependency as a singleton
        /// </summary>
        /// <param name="serviceType">Type of dependency declaration</param>
        /// <param name="implementationType">Type of dependency implementation</param>        
        void RegisterSingleton(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers an instance of dependency
        /// </summary>
        /// <typeparam name="TService">Type of dependency</typeparam>
        /// <param name="instance">Instance of dependency</param>
        void RegisterInstance<TService>(TService instance) where TService : class;

        /// <summary>
        /// Registers an instance of dependency
        /// </summary>
        /// <param name="dependencyType">Type of dependency</param>
        /// <param name="instance">Instance of dependency</param>
        void RegisterInstance(Type dependencyType, object instance);
    }

    /// <summary>
    /// Represents object that is capable of resolving services from the IoC container
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
}
