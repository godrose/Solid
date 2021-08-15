using System.Reflection;
using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    internal sealed class AutomagicalRegistrationScenarioDataStore : ScenarioDataStoreBase
    {
        public AutomagicalRegistrationScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public Assembly[] Assemblies
        {
            get => GetValue<Assembly[]>();
            set => SetValue(value);
        }

        public IIocContainer IocContainer
        {
            get => GetValue<IIocContainer>();
            set => SetValue(value);
        }
    }
}
