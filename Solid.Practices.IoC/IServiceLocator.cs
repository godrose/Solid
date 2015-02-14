using System;
using System.Collections.Generic;

namespace Solid.Practices.IoC
{
    public interface IServiceLocator
    {
        TService GetInstance<TService>(Type serviceType) where TService : class;
        TService GetInstance<TService>() where TService : class;
        object GetInstance(Type serviceType);
        IEnumerable<object> GetAllInstances(Type serviceType);
        void BuildUp(object instance);
    }
}