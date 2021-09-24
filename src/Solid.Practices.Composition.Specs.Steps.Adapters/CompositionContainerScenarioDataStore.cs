using Attest.Testing.SpecFlow;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Specs.Steps.Adapters
{
    internal sealed class CompositionContainerScenarioDataStore : ScenarioDataStoreBase
    {
        public CompositionContainerScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IComposer Container
        {
            get => GetValue<IComposer>();
            set => SetValue(value);
        }

        public IDependencyResolver Resolver
        {
            get => GetValue<IDependencyResolver>();
            set => SetValue(value);
        }
    }
}
