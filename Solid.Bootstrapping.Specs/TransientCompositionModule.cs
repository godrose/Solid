using Solid.Practices.Modularity;

namespace Solid.Bootstrapping.Specs
{
    class TransientCompositionModule : ICompositionModule<FakeContainer>
    {
        public void RegisterModule(FakeContainer iocContainer)
        {
            iocContainer.RegisterTransient<IDependency, TransientDependency>();
        }
    }
}