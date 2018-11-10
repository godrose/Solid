using System.Linq;
using FluentAssertions;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Bootstrapping.Tests
{
    public class ContainerRegistratorExtensionsTests
    {
        [Fact]
        public void
            GivenCompositionModuleRegistersDependencyAsSingleton_WhenCompositionModulesRegistrationIsInvoked_ThenDependencyIsRegistered
            ()
        {
            var container = new FakeIocContainer();
            container.RegisterContainerAdapterCompositionModules(new ICompositionModule[]
            {
                new TransientIocCompositionModule()
            });

            var registrations = container.Registrations;
            var dependencyRegistration = registrations.First();
            dependencyRegistration.ImplementationType.Should().Be(typeof(TransientDependency));
            dependencyRegistration.InterfaceType.Should().Be(typeof(IDependency));
            dependencyRegistration.IsSingleton.Should().Be(false);
        }
    }
}