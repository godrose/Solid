using Solid.Practices.IoC;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.SpecFlow
{
    public sealed class CommonScenarioDataStore<TRootObject> : ScenarioDataStoreBase
    {
        public CommonScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public TRootObject RootObject
        {
            get => GetValue<TRootObject>();
            set => SetValue(value);
        }

        public IIocContainer IocContainer
        {
            get => GetValue<IIocContainer>();
            set => SetValue(value);
        }
    }
}
