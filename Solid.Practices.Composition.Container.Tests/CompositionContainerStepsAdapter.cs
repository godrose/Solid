using System;
using System.Reflection;
using FluentAssertions;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;
using Xunit;

namespace Solid.Practices.Composition.Container.Tests
{
    [Binding]
    internal sealed class CompositionContainerStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public CompositionContainerStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"The composition container is initialized with the current folder")]
        public void WhenTheCompositionContainerIsInitializedWithTheCurrentFolder()
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            var stubTypeInfoExtractionService = _scenarioContext.Get<ITypeInfoExtractionService>("typeInfoExtractionService");
            var stubCompositionModuleExtractionStrategy = _scenarioContext.ContainsKey("moduleCreationStrategy") ? _scenarioContext.Get<ICompositionModuleCreationStrategy>("moduleCreationStrategy") : default;
            ICompositionContainer<ICompositionModule> container = new SimpleCompositionContainer<ICompositionModule>(assemblies,
                stubTypeInfoExtractionService, stubCompositionModuleExtractionStrategy);
            _scenarioContext.Add("compositionContainer", container);
        }

        [When(@"The composition container is composed")]
        public void WhenTheCompositionContainerIsComposed()
        {
            var container = _scenarioContext.Get<ICompositionContainer<ICompositionModule>>("compositionContainer");
            var exception = Record.Exception(() => container.Compose());
            _scenarioContext.Add("exception", exception);
        }

        [Then(@"The exception should be of correct type and have the following message '(.*)'")]
        public void ThenTheExceptionShouldBeOfCorrectTypeAndHaveTheFollowingMessage(string message)
        {
            var exception = _scenarioContext.Get<Exception>("exception");
            exception.Should().BeOfType<AggregateAssemblyInspectionException>().Which.InnerExceptions[0].Message.Should()
                .Be(message);
        }

        [Then(@"The exception should contain info for the second type with message '(.*)'")]
        public void ThenTheExceptionShouldContainInfoForTheSecondTypeWithMessage(string p0)
        {
            var exception = _scenarioContext.Get<Exception>("exception");
            var secondType = _scenarioContext.Get<Type>("secondType");
            var moduleCreationException = exception.As<AggregateAssemblyInspectionException>().InnerExceptions[0]
                .As<AggregateModuleCreationException>().InnerExceptions[0];
            moduleCreationException.Type.Should().Be(secondType);
            moduleCreationException.Message.Should()
                .Be("Unable to create module for the specified type");
        }

    }
}
