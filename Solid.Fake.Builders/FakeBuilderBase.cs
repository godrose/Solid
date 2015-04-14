using Solid.Fake.Core;

namespace Solid.Fake.Builders
{
    public abstract class FakeBuilderBase<TService> where TService : class
    {
        private readonly IFakeFactory _fakeFactory;
        protected readonly IFake<TService> FakeService;

        public FakeBuilderBase(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
            FakeService = _fakeFactory.CreateFake<TService>();
        }

        protected abstract void SetupFake();

        public TService GetService()
        {
            SetupFake();
            return FakeService.Object;
        }
    }
}
