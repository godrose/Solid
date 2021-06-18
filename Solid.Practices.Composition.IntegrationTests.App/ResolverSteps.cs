using FluentAssertions;
using Solid.Practices.Composition.IntegrationTests.Contracts;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class ResolverSteps
    {
        private readonly CompositionContainerScenarioDataStore _scenarioDataStore;

        public ResolverSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new CompositionContainerScenarioDataStore(scenarioContext);
        }

        [Then(@"The loaded placeholder type is OK")]
        public void ThenTheLoadedPlaceholderTypeIsOK()
        {
            var resolver = _scenarioDataStore.Resolver;
            var placeholder = resolver.Resolve<IPlaceholder>();
            var length = placeholder.Length;
            length.Should().Be(5);
        }
    }
}
