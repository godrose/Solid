using Solid.Fake.Core;

namespace Solid.Fake.Builders
{
    public abstract class FakeBuilderBase<TService> where TService : class
    {
        protected readonly IFake<TService> FakeService;

        protected FakeBuilderBase(IFakeFactory fakeFactory)
        {
            FakeService = fakeFactory.CreateFake<TService>();
        }

        protected abstract void SetupFake();

        public TService GetService()
        {
            SetupFake();
            return FakeService.Object;
        }
    }
}
