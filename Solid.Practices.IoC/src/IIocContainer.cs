using System;
using System.Collections.Generic;

namespace Solid.Practices.IoC
{
    /// <summary>
    /// Represents an IoC (Inversion-Of-Control) container.
    /// </summary>
    public interface IIocContainer : IDependencyRegistrator, IDependencyResolver, IDisposable
    {      
          
    }
    
    /// <summary>
    /// Represents an object that is capable of registering dependencies into an IoC container.
    /// </summary>
    public interface IDependencyRegistrator
    {
        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        void RegisterTransient<TDependency, TImplementation>() where TImplementation : class, TDependency;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        void RegisterTransient<TDependency, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TDependency;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        void RegisterTransient<TDependency>() where TDependency : class;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        void RegisterTransient<TDependency>(Func<TDependency> dependencyCreator) where TDependency : class;

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        void RegisterTransient(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers dependency in a transient lifetime style.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator);

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        void RegisterSingleton<TDependency>() where TDependency : class;

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        void RegisterSingleton<TDependency>(Func<TDependency> dependencyCreator) where TDependency : class;

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        void RegisterSingleton<TDependency, TImplementation>() where TImplementation : class, TDependency;

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        void RegisterSingleton<TDependency, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TDependency;

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>        
        void RegisterSingleton(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>      
        void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator);

        /// <summary>
        /// Registers the instance of the dependency.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="instance">The instance of the dependency.</param>
        void RegisterInstance<TDependency>(TDependency instance) where TDependency : class;

        /// <summary>
        /// Registers the instance of the dependency.
        /// </summary>
        /// <param name="dependencyType">The type of dependency.</param>
        /// <param name="instance">The instance of dependency.</param>
        void RegisterInstance(Type dependencyType, object instance);       

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyTypes">The dependency types.</param>
        void RegisterCollection<TDependency>(IEnumerable<Type> dependencyTypes) where TDependency : class;

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencies">The dependencies.</param>
        void RegisterCollection<TDependency>(IEnumerable<TDependency> dependencies) where TDependency : class;

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyType">The type of the dependency.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes);

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyType">The type of the dependency.</param>
        /// <param name="dependencies">The dependencies.</param>
        void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies);
    }

    /// <summary>
    /// Represents object that is capable of resolving dependencies from an IoC container.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Resolves an instance of dependency.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <returns></returns>
        TDependency Resolve<TDependency>() where TDependency : class;

        /// <summary>
        /// Resolves an instance of dependency according to the dependency type.
        /// </summary>
        /// <param name="dependencyType">The type of the dependency.</param>
        /// <returns></returns>
        object Resolve(Type dependencyType);

        /// <summary>
        /// Resolves all instances of dependency.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns></returns>
        IEnumerable<TDependency> ResolveAll<TDependency>() where TDependency : class;

        /// <summary>
        /// Resolves all instances of dependency according to the dependency type.
        /// </summary>
        /// <param name="dependencyType">The type of the dependency</param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type dependencyType);
    }

    /// <summary>
    /// Represents means of registering a dependency whose lifetime is bound
    /// to the lifetime of another object.
    /// </summary>
    public interface IDependencyRegistratorScoped
    {
        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <param name="lifetimeProvider">The lifetime scope.</param>
        /// <param name="dependencyContract">The type of the dependency declaration.</param>
        /// <param name="dependencyImplementation">The type of the dependency implementation.</param>
        void RegisterScoped(Func<object> lifetimeProvider, Type dependencyContract, Type dependencyImplementation);

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        void RegisterScoped<TDependency, TImplementation>(Func<object> lifetimeProvider);

        /// <summary>
        /// Registers the dependency per lifetime of another object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="lifetimeProvider">The lifetime provider.</param>
        void RegisterScoped<TDependency>(Func<object> lifetimeProvider);
    }

    /// <summary>
    /// Represents an adapter to an Inversion-Of-Control container.
    /// This is a marker interface.
    /// </summary>
    public interface IIocContainerAdapter
    {
        
    }

    /// <summary>
    /// Represents an adapter to the concrete IoC container.
    /// This is a marker interface.
    /// </summary>
    /// <typeparam name="TContainer">The type of the concrete IoC container.</typeparam>
    public interface IIocContainerAdapter<TContainer> : IIocContainerAdapter
    {
        
    }
}
