using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping.Tests
{
    class TransientIocCompositionModule : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator iocContainerRegistrator)
        {
            iocContainerRegistrator.RegisterTransient<IDependency, TransientDependency>();
        }
    }   
}