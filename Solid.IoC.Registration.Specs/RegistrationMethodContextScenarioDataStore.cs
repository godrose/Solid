using System;
using Attest.Testing.SpecFlow;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    internal sealed class RegistrationMethodContextScenarioDataStore : ScenarioDataStoreBase
    {
        public RegistrationMethodContextScenarioDataStore(ScenarioContext scenarioContext) 
            : base(scenarioContext)
        {
        }

        public Action<IIocContainer, TypeMatch> LastDefaultRegistrationMethod
        {
            get => GetValue<Action<IIocContainer, TypeMatch>>();
            set => SetValue(value);
        }

        public Exception LastError
        {
            get => GetValue<Exception>();
            set => SetValue(value);
        }
    }
}
