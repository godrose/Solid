using System;
using System.Collections.Generic;
using BoDi;
using Solid.Practices.IoC;

namespace Solid.IoC.Adapters.BoDi
{
    /// <summary>
    /// Represents a container adapter for <see cref="ObjectContainer"/>
    /// </summary>
    /// <seealso cref="IIocContainer" />    
    /// <seealso cref="IIocContainerAdapter" />    
    public class ObjectContainerAdapter : IIocContainer, IIocContainerAdapter<ObjectContainer>
    {
        private readonly ObjectContainer _objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectContainerAdapter"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public ObjectContainerAdapter(ObjectContainer objectContainer) => _objectContainer = objectContainer;

        /// <inheritdoc />       
        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService =>
            _objectContainer.RegisterTypeAs<TImplementation, TService>();

        /// <inheritdoc />             
        public void RegisterTransient<TService>() where TService : class =>
            _objectContainer.RegisterTypeAs<TService, TService>();

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService =>
            _objectContainer.RegisterTypeAs<TImplementation, TService>();

        /// <inheritdoc />       
        public void RegisterInstance<TService>(TService instance) where TService : class =>
            _objectContainer.RegisterInstanceAs(instance);

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class
        {
            foreach (var dependencyType in dependencyTypes)
            {
                _objectContainer.RegisterTypeAs<TService>(dependencyType, dependencyType.Name);
            }
        }

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class =>
            throw new NotImplementedException();

        /// <inheritdoc />        
        public void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes) =>
            throw new NotImplementedException();

        /// <inheritdoc />        
        public void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies) =>
            throw new NotImplementedException();
       
        /// <inheritdoc />       
        public void RegisterInstance(Type dependencyType, object instance) =>
            _objectContainer.RegisterInstanceAs(instance, dependencyType);

        /// <inheritdoc />       
        public void RegisterSingleton(Type serviceType, Type implementationType) =>
            _objectContainer.RegisterTypeAs(implementationType, serviceType);

        /// <inheritdoc />       
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator)
            where TImplementation : class, TService => _objectContainer.RegisterFactoryAs<TService>(dependencyCreator);

        /// <inheritdoc />       
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class =>
            _objectContainer.RegisterFactoryAs(dependencyCreator);

        /// <inheritdoc />        
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator) =>
            throw new NotImplementedException();

        /// <inheritdoc />       
        public void RegisterSingleton<TService>() where TService : class =>
            _objectContainer.RegisterTypeAs<TService, TService>();

        /// <inheritdoc />       
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class =>
            _objectContainer.RegisterFactoryAs(dependencyCreator);

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator)
            where TImplementation : class, TService => _objectContainer.RegisterFactoryAs<TService>(dependencyCreator);

        /// <inheritdoc />         
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator) =>
            _objectContainer.RegisterFactoryAs(dependencyCreator, serviceType);

        /// <inheritdoc />        
        public void RegisterTransient(Type serviceType, Type implementationType) =>
            _objectContainer.RegisterTypeAs(implementationType, serviceType);

        /// <inheritdoc />       
        public TService Resolve<TService>() where TService : class => _objectContainer.Resolve<TService>();

        /// <inheritdoc />        
        public object Resolve(Type dependencyType) => _objectContainer.Resolve(dependencyType);

        /// <inheritdoc />        
        public IEnumerable<T> ResolveAll<T>() where T : class => _objectContainer.ResolveAll<T>();

        /// <inheritdoc />
        public IEnumerable<object> ResolveAll(Type dependencyType) => throw new NotImplementedException();

        /// <inheritdoc />       
        public void Dispose() => _objectContainer.Dispose();
    }
}
