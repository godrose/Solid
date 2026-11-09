#if REQNROLL
using Attest.Testing.Reqnroll;
#else
using Attest.Testing.SpecFlow;
#endif
using Solid.Practices.IoC;
#if REQNROLL
using Reqnroll;
#else
using TechTalk.SpecFlow;
#endif

#if REQNROLL
namespace Solid.IoC.Adapters.Reqnroll.Specs
#else
namespace Solid.IoC.Adapters.BoDi.Specs
#endif
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
