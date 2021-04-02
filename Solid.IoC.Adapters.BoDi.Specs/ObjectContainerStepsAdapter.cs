using System.Linq;
using BoDi;
using FluentAssertions;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Adapters.BoDi.Specs
{
    [Binding]
    internal sealed class ObjectContainerStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public ObjectContainerStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"The new container is created")]
        public void WhenTheNewContainerIsCreated()
        {
            var container = new ObjectContainerAdapter(new ObjectContainer());
            _scenarioContext.Add("container", container);
        }

        [When(@"The services collection is registered")]
        public void WhenTheServicesCollectionIsRegistered()
        {
            var container = _scenarioContext.Get<IIocContainer>("container");
            container.RegisterCollection<IDependency>(new[]
                {typeof(DependencyA), typeof(DependencyB)}, true);
        }

        [Then(@"The services collection should be resolved by implementations")]
        public void ThenTheServicesCollectionShouldBeResolvedByImplementations()
        {
            var container = _scenarioContext.Get<IIocContainer>("container");
            var dependencies = container.ResolveAll<IDependency>().ToArray();
            dependencies[0].Should().BeOfType<DependencyA>();
            dependencies[1].Should().BeOfType<DependencyB>();
        }
    }
}
