using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    internal class MiddlewareTypesScenarioDataStore<TExtensible> : ScenarioDataStoreBase 
        where TExtensible : class
    {
        public MiddlewareTypesScenarioDataStore(ScenarioContext scenarioContext) :
            base(scenarioContext)
        {
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