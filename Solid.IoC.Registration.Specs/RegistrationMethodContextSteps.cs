using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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

        [When(@"I get default registration method for '(.*)'")]
        public void WhenIGetDefaultRegistrationMethodFor(string containerName)
        {
            Exception error = default;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    error = Record.Exception(RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>);
                    break;
                case Consts.MicrosoftDiContainerName:
                    error = Record.Exception(RegistrationMethodContext
                        .GetDefaultRegistrationMethod<IServiceCollection>);
                    break;
            }
            _scenarioDataStore.LastError = error;
        }

        [When(@"I set default registration method for '(.*)'")]
        public void WhenISetDefaultRegistrationMethodFor(string containerName)
        {
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    Action<IIocContainer, TypeMatch> defaultIocContainerRegistrationMethod = (container, match) => { };
                    _scenarioDataStore.LastDefaultRegistrationMethod = defaultIocContainerRegistrationMethod;
                    RegistrationMethodContext.SetDefaultRegistrationMethod(defaultIocContainerRegistrationMethod);
                    break;
                case Consts.MicrosoftDiContainerName:
                    Action<IServiceCollection, TypeMatch> defaultServiceCollectionRegistrationMethod = (container, match) => { };
                    _scenarioDataStore.LastDefaultRegistrationMethod = defaultServiceCollectionRegistrationMethod;
                    RegistrationMethodContext.SetDefaultRegistrationMethod(defaultServiceCollectionRegistrationMethod);
                    break;
            }
        }

        [Then(@"The default registration method for '(.*)' is set")]
        public void ThenTheDefaultRegistrationMethodForIsSet(string containerName)
        {
            Delegate defaultRegistrationMethod = default;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    defaultRegistrationMethod = RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>();
                    break;
                case Consts.MicrosoftDiContainerName:
                    defaultRegistrationMethod =
                        RegistrationMethodContext.GetDefaultRegistrationMethod<IServiceCollection>();
                    break;
            }

            defaultRegistrationMethod.Should().NotBeNull();
        }

        [Then(@"The default registration method for '(.*)' is overridden")]
        public void ThenTheDefaultRegistrationMethodForIsOverridden(string containerName)
        {
            Delegate defaultRegistrationMethod = default;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    defaultRegistrationMethod = RegistrationMethodContext.GetDefaultRegistrationMethod<IIocContainer>();
                    break;
                case Consts.MicrosoftDiContainerName:
                    defaultRegistrationMethod =
                        RegistrationMethodContext.GetDefaultRegistrationMethod<IServiceCollection>();
                    break;
            }
            defaultRegistrationMethod.Should().BeSameAs(_scenarioDataStore.LastDefaultRegistrationMethod);
        }

        [Then(@"There are no errors")]
        public void ThenThereAreNoErrors()
        {
            var lastError = _scenarioDataStore.LastError;
            lastError.Should().BeNull();
        }

        [Then(@"The correspondent error with details for get default registration method for '(.*)' is thrown")]
        public void ThenTheCorrespondentErrorWithDetailsForGetDefaultRegistrationMethodForIsThrown(string containerName)
        {
            string containerType = default;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    containerType = "IIocContainer";
                    break;
                case Consts.MicrosoftDiContainerName:
                    containerType = "IServiceCollection";
                    break;
            }
            var lastError = _scenarioDataStore.LastError;
            lastError.Should().BeOfType<MissingDefaultRegistrationMethodException>()
                .Which.Message.Should().Be($"Missing default registration method for {containerType}");
        }
    }
}