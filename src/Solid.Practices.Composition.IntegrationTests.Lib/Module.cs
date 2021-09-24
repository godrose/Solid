using Moq;
using Solid.Practices.Composition.IntegrationTests.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.IntegrationTests.Lib
{
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            var mock = new Mock<IPlaceholder>();
            mock.Setup(t => t.Length).Returns(5);
            dependencyRegistrator.RegisterInstance(mock.Object);
        }
    }
}
