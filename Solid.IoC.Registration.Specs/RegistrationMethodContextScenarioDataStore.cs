using System;
using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    internal sealed class RegistrationMethodContextScenarioDataStore : ScenarioDataStoreBase
    {
        public RegistrationMethodContextScenarioDataStore(ScenarioContext scenarioContext) 
            : base(scenarioContext)
        {
        }

        public Delegate LastDefaultRegistrationMethod
        {
            get => GetValue<Delegate>();
            set => SetValue(value);
        }

        public Exception LastError
        {
            get => GetValue<Exception>();
            set => SetValue(value);
        }
    }
}
