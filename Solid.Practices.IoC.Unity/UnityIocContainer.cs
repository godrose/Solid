using System;
using Microsoft.Practices.Unity;

namespace Solid.Practices.IoC.Unity
{
    class UnityIocContainer : IIocContainer
    {
        private readonly UnityContainer _container = new UnityContainer();

        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>();
        }

        public void RegisterTransient<TService>() where TService : class
        {
            _container.RegisterType<TService>();
        }

        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            _container.RegisterType(serviceType, implementationType);
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }

        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            _container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }
    }
}
