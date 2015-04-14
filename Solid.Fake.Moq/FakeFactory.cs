using Moq;
using Solid.Data.Fake.Core;

namespace Solid.Fake.Moq
{
    class FakeFactory : IFakeFactory
    {
        public IFake<TFaked> CreateFake<TFaked>() where TFaked : class
        {
            return new Fake<TFaked>(new Mock<TFaked>(MockBehavior.Default));
        }
    }
}
