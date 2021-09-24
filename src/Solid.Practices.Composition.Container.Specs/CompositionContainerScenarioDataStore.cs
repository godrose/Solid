using System;
using Attest.Testing.SpecFlow;
using Solid.Practices.Composition.Contracts;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Container.Specs
{
    internal sealed class CompositionContainerScenarioDataStore : ScenarioDataStoreBase
    {
        public CompositionContainerScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public ITypeInfoExtractionService TypeInfoExtractionService
        {
            get => GetValue<ITypeInfoExtractionService>();
            set => SetValue(value);
        }

        public ICompositionModuleCreationStrategy ModuleCreationStrategy
        {
            get => GetValue<ICompositionModuleCreationStrategy>();
            set => SetValue(value);
        }

        public IComposer CompositionContainer
        {
            get => GetValue<IComposer>();
            set => SetValue(value);
        }

        public Exception Exception
        {
            get => GetValue<Exception>();
            set => SetValue(value);
        }

        public Type FirstType
        {
            get => GetValue<Type>();
            set => SetValue(value);
        }

        public Type SecondType
        {
            get => GetValue<Type>();
            set => SetValue(value);
        }
    }
}
