using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Bootstrapping.Tests
{
    public class MiddlewareTests
    {
        [Fact]
        public void
            GivenCompositionModuleRegistersDependencyAsSingleton_WhenRegisterContainerAdapterCompositionModulesMiddlewareIsApplied_ThenDependencyIsRegistered
            ()
        {
            var container = new FakeIocContainer();
            var bootstrapper = new FakeBootstrapper
            {
                Registrator = container,
                Modules = new ICompositionModule[]
                {
                    new TransientIocCompositionModule()
                }
            };

            var middleware = new RegisterCompositionModulesMiddleware<FakeBootstrapper>();
            middleware.Apply(bootstrapper);

            var registrations = container.Registrations;
            var dependencyRegistration = registrations.First();
            dependencyRegistration.ImplementationType.Should().Be(typeof(TransientDependency));
            dependencyRegistration.InterfaceType.Should().Be(typeof(IDependency));
            dependencyRegistration.IsSingleton.Should().Be(false);
        }        

        [Fact]
        public void
            GivenThereAreTwoServicesThatImplementTheContract_WhenRegisterCollectionMiddlewareIsApplied_ThenBothServicesAreRegistered
            ()
        {
            var containerAdapter = new FakeIocContainer();
            var bootstrapper = new FakeBootstrapper
            {
                Registrator = containerAdapter,
                Assemblies = new[] { typeof(FakeIocContainer).GetTypeInfo().Assembly }
            };

            var middleware = new RegisterCollectionMiddleware<FakeBootstrapper>(typeof(IServiceContract));
            middleware.Apply(bootstrapper);

            var registrations = containerAdapter.Registrations;
            var dependencyRegistration = registrations.First();
            (dependencyRegistration.InterfaceType == typeof(IEnumerable<IServiceContract>)).Should().BeTrue();
        }
    }
}

