using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    internal sealed class ExtensibilityByTypeScenarioDataStore : ScenarioDataStoreBase
    {
        public ExtensibilityByTypeScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public FakeBootstrapperWithExtensibilityByType Bootstrapper
        {
            get => GetValue<FakeBootstrapperWithExtensibilityByType>();
            set => SetValue(value);
        }
    }
}
