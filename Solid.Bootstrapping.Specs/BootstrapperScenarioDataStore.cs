using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    internal sealed class BootstrapperScenarioDataStore : ScenarioDataStoreBase
    {
        public BootstrapperScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IDependencyRegistrator Container
        {
            get => GetValue<IDependencyRegistrator>();
            set => SetValue(value);
        }

        public FakeBootstrapper Bootstrapper
        {
            get => GetValue<FakeBootstrapper>();
            set => SetValue(value);
        }
    }
}
