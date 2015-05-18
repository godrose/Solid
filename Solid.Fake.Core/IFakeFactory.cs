namespace Solid.Fake.Core
{
    public interface IFakeFactory
    {
        IFake<TFaked> CreateFake<TFaked>() where TFaked : class;
        IMock<TFaked> CreateMock<TFaked>() where TFaked : class;
    }
}