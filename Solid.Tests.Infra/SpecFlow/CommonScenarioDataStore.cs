using Solid.Practices.IoC;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.SpecFlow
{
    public sealed class CommonScenarioDataStore<TExtensible> : ScenarioDataStoreBase
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
