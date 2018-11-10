using System.Linq;
using FluentAssertions;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Bootstrapping.Tests
{
    public class ContainerExtensionsTests
    {
        [Fact]
        public void
            GivenCompositionModuleRegistersDependencyAsSingleton_WhenCompositionModulesRegistrationIsInvoked_ThenDependencyIsRegistered
            ()
        {
            var container = new FakeContainer();
            container.RegisterContainerCompositionModules(new ICompositionModule[]
            {
                new TransientCompositionModule()
            });

            var registrations = container.Registrations;
            var dependencyRegistration = registrations.First();
            dependencyRegistration.ImplementationType.Should().Be(typeof(TransientDependency));
            dependencyRegistration.InterfaceType.Should().Be(typeof(IDependency));
            dependencyRegistration.IsSingleton.Should().Be(false);
        }
    }
}