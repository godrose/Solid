using Solid.Fake.Builders;
using Solid.Fake.Core;
using Solid.Practices.IoC;

namespace Solid.Tests.Core
{
    public abstract class IntegrationTestsBase<TContainer, TFakeFactory, TRootObject>
        where TContainer : IIocContainer, new()
        where TFakeFactory : IFakeFactory, new()
        where TRootObject : class
    {
        //defensive initialization
        protected TContainer IocContainer = new TContainer();

        protected TRootObject CreateRootObject()
        {
            var rootObject = CreateRootObjectCore();
            return CreateRootObjectOverride(rootObject);
        }

        private TRootObject CreateRootObjectCore()
        {
            return Resolve<TRootObject>();
        }

        protected virtual TRootObject CreateRootObjectOverride(TRootObject rootObject)
        {
            return rootObject;
        }
        
        protected abstract void Setup();
        protected abstract void TearDown();

        protected void RegisterService<TService>(TService service) where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterService(IocContainer, service);
        }

        protected void RegisterBuilder<TService>(FakeBuilderBase<TService> builder) where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterBuilder(IocContainer, builder);
        }

        protected void RegisterStub<TService>() where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterStub<TService>(IocContainer);
        }

        protected void RegisterFake<TService>(IFake<TService> fake) where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterFake(IocContainer, fake);
        }

        protected void RegisterMock<TService>(IMock<TService> fake) where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterMock(IocContainer, fake);
        }

        protected TService Resolve<TService>() where TService : class
        {
            return IntegrationTestsHelper<TFakeFactory>.Resolve<TService>(IocContainer);
        }
    }
}
