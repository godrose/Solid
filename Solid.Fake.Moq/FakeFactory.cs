using Moq;
using Solid.Fake.Core;

namespace Solid.Fake.Moq
{
    public class FakeFactory : IFakeFactory
    {
        public IFake<TFaked> CreateFake<TFaked>() where TFaked : class
        {
            return new Fake<TFaked>(new Mock<TFaked>(MockBehavior.Default));
        }
    }
}
