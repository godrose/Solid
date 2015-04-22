namespace Solid.Fake.Core
{
    public interface IFakeFactory
    {
        IFake<TFaked> CreateFake<TFaked>() where TFaked : class;
    }
}