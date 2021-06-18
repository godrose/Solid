using System.Reflection;
using FluentAssertions;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;
using Xunit;

namespace Solid.Practices.Composition.Container.Specs
{
    [Binding]
    internal sealed class CompositionContainerSteps
    {
        private readonly CompositionContainerScenarioDataStore _scenarioDataStore;

        public CompositionContainerSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new CompositionContainerScenarioDataStore(scenarioContext);
        }

        [When(@"The composition container is initialized with the current folder")]
        public void WhenTheCompositionContainerIsInitializedWithTheCurrentFolder()
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly() };
            var stubTypeInfoExtractionService = _scenarioDataStore.TypeInfoExtractionService;
            var stubCompositionModuleExtractionStrategy = _scenarioDataStore.ModuleCreationStrategy;
            ICompositionContainer<ICompositionModule> container = new SimpleCompositionContainer<ICompositionModule>(assemblies,
                stubTypeInfoExtractionService, stubCompositionModuleExtractionStrategy);
            _scenarioDataStore.CompositionContainer = container;
        }

        [When(@"The composition container is composed")]
        public void WhenTheCompositionContainerIsComposed()
        {
            var container = _scenarioDataStore.CompositionContainer;
            var exception = Record.Exception(() => container.Compose());
            _scenarioDataStore.Exception = exception;
        }

        [Then(@"The exception should be of correct type and have the following message '(.*)'")]
        public void ThenTheExceptionShouldBeOfCorrectTypeAndHaveTheFollowingMessage(string message)
        {
            var exception = _scenarioDataStore.Exception;
            exception.Should().BeOfType<AggregateAssemblyInspectionException>().Which.InnerExceptions[0].Message.Should()
                .Be(message);
        }

        [Then(@"The exception should contain info for the second type with message '(.*)'")]
        public void ThenTheExceptionShouldContainInfoForTheSecondTypeWithMessage(string expectedMessage)
        {
            var exception = _scenarioDataStore.Exception;
            var secondType = _scenarioDataStore.SecondType;
            var moduleCreationException = exception.As<AggregateAssemblyInspectionException>().InnerExceptions[0]
                .As<AggregateModuleCreationException>().InnerExceptions[0];
            moduleCreationException.Type.Should().Be(secondType);
            moduleCreationException.Message.Should()
                .Be(expectedMessage);
        }

    }
}
