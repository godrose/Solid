using FluentAssertions;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Specs.Steps.Adapters
{
    [Binding]
    internal sealed class DiscoveryAspectStepsAdapter
    {
        private readonly ScenarioContext _scenarioContext;

        public DiscoveryAspectStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"The discovery aspect is created")]
        public void WhenTheDiscoveryAspectIsCreated()
        {
            var compositionOptions = new CompositionOptions
            {
                Prefixes = new[] { "Solid" }
            };
            var discoveryAspect = new DiscoveryAspect(compositionOptions);
            _scenarioContext.Add("discoveryAspect", discoveryAspect);
        }

        [When(@"The discovery aspect is initialized")]
        public void WhenTheDiscoveryAspectIsInitialized()
        {
            var discoveryAspect = _scenarioContext.Get<DiscoveryAspect>("discoveryAspect");
            discoveryAspect.Initialize();
        }

        [Then(@"The library assembly is discovered")]
        public void ThenTheLibraryAssemblyIsDiscovered()
        {
            var discoveryAspect = _scenarioContext.Get<DiscoveryAspect>("discoveryAspect");
            var assemblies = discoveryAspect.Assemblies;
            assemblies.Should().Contain(t => t.FullName.Contains("Solid.Practices.Composition.IntegrationTests.Lib"));
        }

        [Then(@"There should be (.*) assemblies")]
        public void ThenThereShouldBeAssemblies(int expectedCount)
        {
            var discoveryAspect = _scenarioContext.Get<DiscoveryAspect>("discoveryAspect");
            var assemblies = discoveryAspect.Assemblies;
            assemblies.Should().HaveCount(expectedCount);
        }
    }
}
