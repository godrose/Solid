using System;
using FluentAssertions;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;
using Xunit;

namespace Solid.IoC.Registration.Specs
{
    [Binding]
    internal sealed class RegistrationMethodContextSteps
    {
        private readonly RegistrationMethodContextScenarioDataStore _scenarioDataStore;

        public RegistrationMethodContextSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new RegistrationMethodContextScenarioDataStore(scenarioContext);
        }

        [When(@"I get default registration method for an ioc container")]
        public void WhenIGetDefaultRegistrationMethodForAnIocContainer()
        {
            var error = Record.Exception(RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>);
            _scenarioDataStore.LastError = error;
        }

        [When(@"I set default registration method for an ioc container")]
        public void WhenISetDefaultRegistrationMethodForAnIocContainer()
        {
            Action<IIocContainer, TypeMatch> defaultRegistrationMethod = (container, match) => { };
            _scenarioDataStore.LastDefaultRegistrationMethod = defaultRegistrationMethod;
            RegistrationMethodContext.SetDefaultRegistrationMethod(defaultRegistrationMethod);
        }

        [Then(@"The default registration method for an ioc container is set")]
        public void ThenTheDefaultRegistrationMethodForAnIocContainerIsSet()
        {
            var defaultRegistrationMethod = RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>();
            defaultRegistrationMethod.Should().NotBeNull();
        }

        [Then(@"The default registration method for an ioc container is overridden")]
        public void ThenTheDefaultRegistrationMethodForAnIocContainerIsOverridden()
        {
            var defaultRegistrationMethod = RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>();
            defaultRegistrationMethod.Should().BeSameAs(_scenarioDataStore.LastDefaultRegistrationMethod);
        }

        [Then(@"There are no errors")]
        public void ThenThereAreNoErrors()
        {
            var lastError = _scenarioDataStore.LastError;
            lastError.Should().BeNull();
        }

        [Then(@"The correspondent error with details for get default registration method is thrown")]
        public void ThenTheCorrespondentErrorWithDetailsForGetDefaultRegistrationMethodIsThrown()
        {
            var lastError = _scenarioDataStore.LastError;
            lastError.Should().BeOfType<MissingDefaultRegistrationMethodException>()
                .Which.Message.Should().Be("Missing default registration method for IIocContainer");
        }
    }
}