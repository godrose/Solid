using Attest.Testing.SpecFlow;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeAspectScenarioDataStore<TExtensible> : ScenarioDataStoreBase
        where TExtensible : class
    {
        public ExtensibilityByTypeAspectScenarioDataStore(ScenarioContext scenarioContext) :
            base(scenarioContext)
        {
        }

        public ExtensibilityByTypeAspect<TExtensible> Aspect
        {
            get => GetValue<ExtensibilityByTypeAspect<TExtensible>>();
            set => SetValue(value);
        }
    }
}