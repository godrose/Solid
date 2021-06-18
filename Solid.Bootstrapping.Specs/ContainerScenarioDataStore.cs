using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    internal sealed class ContainerScenarioDataStore : ScenarioDataStoreBase
    {
        public ContainerScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IRegistrationCollection Container
        {
            get => GetValue<IRegistrationCollection>();
            set => SetValue(value);
        }
    }
}
