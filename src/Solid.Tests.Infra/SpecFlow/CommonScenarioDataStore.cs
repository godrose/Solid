using Solid.Practices.IoC;
#if REQNROLL
using Reqnroll;
#else
using TechTalk.SpecFlow;
#endif

// ReSharper disable once CheckNamespace
#if REQNROLL
namespace Attest.Testing.Reqnroll
#else
namespace Attest.Testing.SpecFlow
#endif
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
