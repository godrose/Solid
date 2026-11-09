using System.Linq;
#if REQNROLL
using Reqnroll.BoDi;
#else
using BoDi;
#endif
using FluentAssertions;
using Solid.Practices.IoC;
#if REQNROLL
using Reqnroll;
#else
using TechTalk.SpecFlow;
#endif

#if REQNROLL
namespace Solid.IoC.Adapters.Reqnroll.Specs
#else
namespace Solid.IoC.Adapters.BoDi.Specs
#endif
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
                new[]
                {
                    typeof(DependencyA),
                    typeof(DependencyB)
                });
        }
    }
}