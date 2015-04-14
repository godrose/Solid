using Solid.Data.Fake.Core;
using Solid.Fake.Moq;

namespace Solid.Fake.Builders
{
    public abstract class FakeBuilderBase<TService> where TService : class
    {
        protected readonly IFake<TService> FakeService = FakeProvider.CreateFake<TService>();

        protected abstract void SetupFake();

        public TService GetService()
        {
            SetupFake();
            return FakeService.Object;
        }
    }
}
