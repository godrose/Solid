using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.IoC.Adapters.BoDi;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class MiddlewareTypesSteps
    {
        private readonly MiddlewareTypeScenarioDataStore<ExtensibleByTypeObject> _scenarioDataStore;

        public MiddlewareTypesSteps(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer)
        {
            _scenarioDataStore = new MiddlewareTypeScenarioDataStore<ExtensibleByTypeObject>(scenarioContext);
            var containerAdapter = new ObjectContainerAdapter(objectContainer);
            _scenarioDataStore.IocContainer = containerAdapter;
        }

        [When(@"The middleware types wrapper is created")]
        [UsedImplicitly]
        public void WhenTheMiddlewareTypesWrapperIsCreated()
        {
            var middlewareTypesWrapper =
                new MiddlewareTypesWrapper<ExtensibleByTypeObject>(_scenarioDataStore.IocContainer);
            _scenarioDataStore.MiddlewareTypesWrapper = middlewareTypesWrapper;
        }

        [When(@"I use a creatable middleware by specifying its type only")]
        public void WhenIUseACreatableMiddlewareBySpecifyingItsTypeOnly()
        {
            _scenarioDataStore.MiddlewareTypesWrapper.Use<CreatableMiddleware>();
        }

        [When(@"I ensure the middlewares are created")]
        public void WhenIEnsureTheMiddlewaresAreCreated()
        {
            var middlewaresReadOnlyCollection = _scenarioDataStore.MiddlewareTypesWrapper.Build();
            _scenarioDataStore.MiddlewaresReadOnlyCollection = middlewaresReadOnlyCollection;
        }

        [Then(@"This middleware is created successfully")]
        public void ThenThisMiddlewareIsCreatedSuccessfully()
        {
            _scenarioDataStore
                .MiddlewaresReadOnlyCollection
                .Middlewares
                .Should()
                .ContainSingle(t => t is CreatableMiddleware);
        }
    }
}