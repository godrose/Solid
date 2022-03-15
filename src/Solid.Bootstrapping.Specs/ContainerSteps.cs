using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [Binding]
    internal sealed class ContainerSteps
    {
        private readonly ContainerScenarioDataStore _scenarioDataStore;

        public ContainerSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new ContainerScenarioDataStore(scenarioContext);
        }

        [When(@"The new container is created")]
        public void WhenTheNewContainerIsCreated()
        {
            var container = new FakeContainer();
            _scenarioDataStore.Container = container;
        }

        [When(@"The new container adapter is created")]
        public void WhenTheNewContainerAdapterIsCreated()
        {
            var containerAdapter = new FakeIocContainer();
            _scenarioDataStore.Container = containerAdapter;
        }

        [When(@"The composition modules are registered for the container")]
        public void WhenTheCompositionModulesAreRegisteredForTheContainer()
        {
            var container = _scenarioDataStore.Container as FakeContainer;
            container.RegisterContainerCompositionModules(new ICompositionModule[]
            {
                new TransientCompositionModule()
            });
        }

        [When(@"The composition modules are registered for the container adapter")]
        public void WhenTheCompositionModulesAreRegisteredForTheContainerAdapter()
        {
            var containerAdapter = _scenarioDataStore.Container as FakeIocContainer;
            containerAdapter.RegisterContainerAdapterCompositionModules(new ICompositionModule[]
            {
                new TransientIocCompositionModule()
            });
        }

        [Then(@"The registered dependency should be of correct type")]
        public void ThenTheRegisteredDependencyShouldBeOfCorrectType()
        {
            var dependencyRegistration = GetDependencyRegistration();
            dependencyRegistration.ImplementationType.Should().Be(typeof(TransientDependency));
            dependencyRegistration.InterfaceType.Should().Be(typeof(IDependency));
        }

        [Then(@"The registered dependency should be transient")]
        public void ThenTheRegisteredDependencyShouldBeTransient()
        {
            var dependencyRegistration = GetDependencyRegistration();
            dependencyRegistration.IsSingleton.Should().Be(false);
        }

        [Then(@"The dependencies are registered as a collection")]
        public void ThenTheDependenciesAreRegisteredAsACollection()
        {
            var dependencyRegistration = GetDependencyRegistration();
            (dependencyRegistration.InterfaceType == typeof(IEnumerable<IServiceContract>)).Should().BeTrue();
        }

        private ContainerEntry GetDependencyRegistration()
        {
            var container = _scenarioDataStore.Container;
            var registrations = container.Registrations;
            var dependencyRegistration = registrations.First();
            return dependencyRegistration;
        }
    }
}
