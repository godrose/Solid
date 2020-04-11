using FluentAssertions;
using Solid.Practices.Composition.IntegrationTests.Contracts;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class ResolverStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public ResolverStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"The loaded placeholder type is OK")]
        public void ThenTheLoadedPlaceholderTypeIsOK()
        {
            var resolver = _scenarioContext.Get<IDependencyResolver>("resolver");
            var placeholder = resolver.Resolve<IPlaceholder>();
            var length = placeholder.Length;
            length.Should().Be(5);
        }
    }
}
