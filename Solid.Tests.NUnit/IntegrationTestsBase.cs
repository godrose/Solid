using NUnit.Framework;
using Solid.Fake.Builders;
using Solid.Fake.Core;
using Solid.Practices.IoC;
using Solid.Tests.Core;

namespace Solid.Tests.NUnit
{
    public abstract class IntegrationTestsBase<TContainer, TFakeFactory, TRootObject>       
        where TContainer : IIocContainer, new()
        where TFakeFactory : IFakeFactory, new() where TRootObject : class
    {
        protected TContainer IocContainer;                

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

        [SetUp]
        protected void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        [TearDown]
        protected void TearDown()
        {
            TearDownCore();
            TearDownOverride();
        }

        private void SetupCore()
        {
            IocContainer = new TContainer();
            new TestBootstrapper<TContainer>(IocContainer);
        }

        protected virtual void SetupOverride()
        {
            
        }

        private void TearDownCore()
        {
            //Dispose();
        }

        protected virtual void TearDownOverride()
        {
            
        }

        protected void RegisterService<TService>(TService service) where TService : class
        {
            IntegrationTestsHelper<TFakeFactory>.RegisterService(IocContainer,service);
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

        protected TService Resolve<TService>() where TService : class
        {
            return IntegrationTestsHelper<TFakeFactory>.Resolve<TService>(IocContainer);
        }
    }
}
