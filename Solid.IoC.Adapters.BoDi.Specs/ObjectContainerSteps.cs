using System.Linq;
using BoDi;
using FluentAssertions;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Adapters.BoDi.Specs
{
    [Binding]
    internal sealed class ObjectContainerSteps
    {
        private readonly ContainerScenarioDataStore _scenarioDataStore;

        public ObjectContainerSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new ContainerScenarioDataStore(scenarioContext);
        }

        [When(@"The new container is created")]
        public void WhenTheNewContainerIsCreated()
        {
            var container = new ObjectContainerAdapter(new ObjectContainer());
            _scenarioDataStore.Container = container;
        }

        [When(@"The services collection is registered")]
        public void WhenTheServicesCollectionIsRegistered()
        {
            var container = _scenarioDataStore.Container;
            container.RegisterCollection<IDependency>(new[]
                {typeof(DependencyA), typeof(DependencyB)}, true);
        }

        [Then(@"The services collection should be resolved by implementations")]
        public void ThenTheServicesCollectionShouldBeResolvedByImplementations()
        {
            var container = _scenarioDataStore.Container;
            var dependencies = container.ResolveAll<IDependency>().ToArray();
            dependencies.Select(d => 
                d.GetType()).Should().BeEquivalentTo(
                typeof(DependencyA), 
                                typeof(DependencyB));
        }
    }
}
