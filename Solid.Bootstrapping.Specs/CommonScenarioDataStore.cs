using Attest.Testing.SpecFlow;
using JetBrains.Annotations;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [UsedImplicitly]
    internal sealed class CommonScenarioDataStore<TExtensible> : ScenarioDataStoreBase
    {
        public CommonScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public TExtensible Object
        {
            get => GetValue<TExtensible>();
            set => SetValue(value);
        }

        public IIocContainer IocContainer
        {
            get => GetValue<IIocContainer>();
            set => SetValue(value);
        }
    }
}
