using Moq;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.IntegrationTests.Lib
{
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            var mock = new Mock<IPlaceHolder>();
            mock.Setup(t => t.Length).Returns(5);
        }
    }

    public interface IPlaceHolder
    {
        int Length { get; }
    }
}
