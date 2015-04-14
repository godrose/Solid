using Solid.Data.Fake.Core;

namespace Solid.Fake.FakeItEasy
{
    public class FakeFactory : IFakeFactory
    {
        public IFake<TFaked> CreateFake<TFaked>() where TFaked : class
        {
            return new Fake<TFaked>();
        }
    }
}
