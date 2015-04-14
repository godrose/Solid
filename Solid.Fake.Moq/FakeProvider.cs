using Moq;
using Solid.Data.Fake.Core;

namespace Solid.Fake.Moq
{
    public static class FakeProvider
    {
        public static IFake<TFaked> CreateFake<TFaked>() where TFaked : class
        {
            return new Fake<TFaked>(CreateMock<TFaked>());
        }

        private static Mock<TFaked> CreateMock<TFaked>() where TFaked : class
        {
            return new Mock<TFaked>(MockBehavior.Default);
        }
    }
}
