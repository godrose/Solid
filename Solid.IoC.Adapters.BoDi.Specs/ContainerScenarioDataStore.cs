using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Adapters.BoDi.Specs
{
    internal sealed class ContainerScenarioDataStore : ScenarioDataStoreBase
    {
        public ContainerScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IIocContainer Container
        {
            get => GetValue<IIocContainer>();
            set => SetValue(value);
        }
    }
}
