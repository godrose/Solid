using System;
using System.Collections.Generic;
using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    internal class AspectsScenarioDataStore : ScenarioDataStoreBase
    {
        public AspectsScenarioDataStore(ScenarioContext scenarioContext) :
            base(scenarioContext)
        {
        }

        public List<string> Callbacks
        {
            get => GetValue<List<string>>();
            set => SetValue(value);
        }

        public List<IAspect> Aspects
        {
            get => GetValue<List<IAspect>>();
            set => SetValue(value);
        }

        public Exception Error
        {
            get => GetValue<Exception>();
            set => SetValue(value);
        }
    }
}