using System;
using System.Reflection;
using FakeItEasy;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Container.Specs
{
    [Binding]
    internal sealed class GivenCompositionContainerStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public GivenCompositionContainerStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"That any type loading from an assembly would throw an exception")]
        public void GivenThatAnyTypeLoadingFromAnAssemblyWouldThrowAnException()
        {
            var stubTypeInfoExtractionService = A.Fake<ITypeInfoExtractionService>();
            A.CallTo(() => stubTypeInfoExtractionService.GetTypes(A<Assembly>._)).Throws<Exception>();
            _scenarioContext.Add("typeInfoExtractionService", stubTypeInfoExtractionService);
        }

        //TODO: Refactor
        [Given(@"There are two types in two modules and loading the first one is Ok and loading of the second throws an exception")]
        public void GivenThereAreTwoTypesInTwoModulesAndLoadingTheFirstOneIsOkAndLoadingOfTheSecondThrowsAnException()
        {
            var stubTypeInfoExtractionService = A.Fake<ITypeInfoExtractionService>();
            var firstType = string.Empty.GetType().GetTypeInfo();
            var secondType = default(int).GetType().GetTypeInfo();
            var types = new[] { firstType, secondType };
            A.CallTo(() => stubTypeInfoExtractionService.GetTypes(A<Assembly>._)).Returns(types);
            A.CallTo(() => stubTypeInfoExtractionService.IsCompositionModule(firstType, typeof(ICompositionModule)))
                .Returns(true);
            A.CallTo(() => stubTypeInfoExtractionService.IsCompositionModule(secondType, typeof(ICompositionModule)))
                .Returns(true);
            var stubCompositionModuleCreationStrategy = A.Fake<ICompositionModuleCreationStrategy>();
            A.CallTo(() => stubCompositionModuleCreationStrategy.CreateCompositionModule(firstType.AsType()))
                .Returns(A.Fake<ICompositionModule>());
            A.CallTo(() => stubCompositionModuleCreationStrategy.CreateCompositionModule(secondType.AsType()))
                .Throws<Exception>();
            _scenarioContext.Add("firstType", firstType);
            _scenarioContext.Add("secondType", secondType);
            _scenarioContext.Add("typeInfoExtractionService", stubTypeInfoExtractionService);
            _scenarioContext.Add("moduleCreationStrategy", stubCompositionModuleCreationStrategy);
        }
    }
}
