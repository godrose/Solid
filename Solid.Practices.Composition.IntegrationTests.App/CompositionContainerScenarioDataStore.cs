using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    internal sealed class CompositionContainerScenarioDataStore : ScenarioDataStoreBase
    {
        public CompositionContainerScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IDependencyResolver Resolver
        {
            get => GetValue<IDependencyResolver>();
            set => SetValue(value);
        }
    }
}
