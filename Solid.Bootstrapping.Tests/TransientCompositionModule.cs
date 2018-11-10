using Solid.Practices.Modularity;

namespace Solid.Bootstrapping.Tests
{
    class TransientCompositionModule : ICompositionModule<FakeContainer>
    {
        public void RegisterModule(FakeContainer iocContainer)
        {
            iocContainer.RegisterTransient<IDependency, TransientDependency>();
        }
    }
}