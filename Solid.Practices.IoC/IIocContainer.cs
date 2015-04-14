using System;

namespace Solid.Practices.IoC
{
    public interface IIocContainer : IIocContainerRegistrator, IIoContainerResolver
    {      
          
    }

    public interface IIocContainerRegistrator
    {
        void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService;
        void RegisterTransient<TService>() where TService : class;
        void RegisterTransient(Type serviceType, Type implementationType);
        void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService;
        void RegisterInstance<TService>(TService instance) where TService : class;
    }

    public interface IIoContainerResolver
    {
        TService Resolve<TService>() where TService : class;
    }
}
