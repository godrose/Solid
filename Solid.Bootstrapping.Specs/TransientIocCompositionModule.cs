using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping.Specs
{
    class TransientIocCompositionModule : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator iocContainerRegistrator)
        {
            iocContainerRegistrator.RegisterTransient<IDependency, TransientDependency>();
        }
    }
}