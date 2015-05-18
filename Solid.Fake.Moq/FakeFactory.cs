using Moq;
using Solid.Fake.Core;

namespace Solid.Fake.Moq
{
    public class FakeFactory : IFakeFactory
    {
        public IFake<TFaked> CreateFake<TFaked>() where TFaked : class
        {
            return CreateFakeImpl<TFaked>();
        }

        public Core.IMock<TFaked> CreateMock<TFaked>() where TFaked : class
        {
            return CreateFakeImpl<TFaked>();
        }

        private static IFake<TFaked> CreateFakeImpl<TFaked>() where TFaked : class
        {
            return new Fake<TFaked>(new Mock<TFaked>(MockBehavior.Default));
        }
    }
}
