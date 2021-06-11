using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    internal class MiddlewareTypeScenarioDataStore<TExtensible> : ScenarioDataStoreBase where TExtensible : class
    {
        public MiddlewareTypeScenarioDataStore(ScenarioContext scenarioContext) :
            base(scenarioContext)
        {
        }

        public IIocContainer IocContainer
        {
            get => GetValue<IIocContainer>();
            set => SetValue(value);
        }

        public MiddlewareTypesWrapper<TExtensible> MiddlewareTypesWrapper
        {
            get => GetValue<MiddlewareTypesWrapper<TExtensible>>();
            set => SetValue(value);
        }

        public MiddlewaresReadOnlyCollection<TExtensible> MiddlewaresReadOnlyCollection
        {
            get => GetValue<MiddlewaresReadOnlyCollection<TExtensible>>();
            set => SetValue(value);
        }
    }
}