using Solid.Fake.Builders;
using Solid.Fake.Core;
using Solid.Practices.IoC;

namespace Solid.Tests.Core
{
    public static class IntegrationTestsHelper<TFakeFactory> where TFakeFactory : IFakeFactory, new()
    {
        private static readonly TFakeFactory FakeFactory = new TFakeFactory();
       
        public static void RegisterService<TService>(IIocContainer container, TService service) where TService : class
        {
            container.RegisterInstance(service);
        }

        public static  void RegisterBuilder<TService>(IIocContainer container, FakeBuilderBase<TService> builder) where TService : class
        {
            RegisterService(container, builder.GetService());
        }

        public static void RegisterStub<TService>(IIocContainer container) where TService : class
        {
            RegisterFake(container, FakeFactory.CreateFake<TService>());
        }

        public static void RegisterFake<TService>(IIocContainer container, IFake<TService> fake) where TService : class
        {
            RegisterHaveFake(container, fake);
        }

        public static void RegisterMock<TService>(IIocContainer container, IMock<TService> fake) where TService : class
        {
            RegisterHaveFake(container, fake);
        }

        private static void RegisterHaveFake<TService>(IIocContainer container, IHaveFake<TService> fake) where TService : class
        {
            RegisterService(container, fake.Object);
        }

        public static TService Resolve<TService>(IIocContainer container) where TService : class
        {
            return container.Resolve<TService>();
        }
    }
}