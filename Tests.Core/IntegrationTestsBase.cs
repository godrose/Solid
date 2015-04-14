using NUnit.Framework;
using Solid.Data.Fake.Core;
using Solid.Fake.Builders;
using Solid.Fake.Moq;
using Solid.Practices.IoC;

namespace Tests.Core
{
    public abstract class IntegrationTestsBase<TContainer> where TContainer : IIocContainer
    {
        protected TContainer IocContainer;

        protected void RegisterService<TService>(TService service) where TService : class
        {
            IocContainer.RegisterInstance(service);
        }

        protected void RegisterBuilder<TService>(FakeBuilderBase<TService> builder) where TService : class
        {
            RegisterService(builder.GetService());
        }

        protected void RegisterStub<TService>() where TService : class
        {
            RegisterFake(FakeProvider.CreateFake<TService>());
        }

        protected void RegisterFake<TService>(IFake<TService> fake) where TService : class
        {
            RegisterService(fake.Object);
        }

        protected TService Resolve<TService>()
        {
            return IocContainer.Resolve<TService>();
        }
    }

    public abstract class IntegrationTestsBase<TContainer, TRootObject> : IntegrationTestsBase<TContainer> where TContainer : IIocContainer, new()
    {
        protected TRootObject CreateRootObject()
        {
            return Resolve<TRootObject>();
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


    }
}
