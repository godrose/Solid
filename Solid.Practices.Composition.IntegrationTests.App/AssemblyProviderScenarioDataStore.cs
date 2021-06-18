using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    internal sealed class AssemblyProviderScenarioDataStore : ScenarioDataStoreBase
    {
        public AssemblyProviderScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public CustomAssemblySourceProvider AssemblySourceProvider
        {
            get => GetValue<CustomAssemblySourceProvider>();
            set => SetValue(value);
        }
    }
}
