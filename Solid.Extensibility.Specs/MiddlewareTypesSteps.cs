using FluentAssertions;
using JetBrains.Annotations;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class MiddlewareTypesSteps
    {
        private readonly IIocContainer _iocContainer;
        private readonly ExtensibleByTypeObject _object;
        private readonly MiddlewareTypesScenarioDataStore<ExtensibleByTypeObject> _middlewareTypesScenarioDataStore;

        public MiddlewareTypesSteps(
            IIocContainer iocContainer,
            ExtensibleByTypeObject @object,
            MiddlewareTypesScenarioDataStore<ExtensibleByTypeObject> middlewareTypesScenarioDataStore)
        {
            _iocContainer = iocContainer;
            _object = @object;
            _middlewareTypesScenarioDataStore = middlewareTypesScenarioDataStore;
        }

        [Given(@"The creatable middleware can be created")]
        [UsedImplicitly]
        public void GivenTheCreatableMiddlewareCanBeCreated()
        {
            _iocContainer
                .RegisterSingleton<ICreatableMiddlewareDependency, CreatableMiddlewareDependency>();
        }

        [Given(@"The creatable middleware can not be created")]
        [UsedImplicitly]
        public void GivenTheCreatableMiddlewareCanNotBeCreated()
        {
            _iocContainer
                .RegisterSingleton<ICreatableMiddlewareDependency, ICreatableMiddlewareDependency>();
        }

        [When(@"The middleware types wrapper is created")]
        [UsedImplicitly]
        public void WhenTheMiddlewareTypesWrapperIsCreated()
        {
            var middlewareTypesWrapper =
                new MiddlewareTypesWrapper<ExtensibleByTypeObject>(_object,
                    _iocContainer);
            _middlewareTypesScenarioDataStore.MiddlewareTypesWrapper = middlewareTypesWrapper;
        }

        [When(@"I use a creatable middleware by specifying its type only")]
        public void WhenIUseACreatableMiddlewareBySpecifyingItsTypeOnly()
        {
            _middlewareTypesScenarioDataStore.MiddlewareTypesWrapper.Use<CreatableMiddleware>();
        }

        [When(@"I ensure the middlewares are created")]
        public void WhenIEnsureTheMiddlewaresAreCreated()
        {
            var middlewaresReadOnlyCollection = _middlewareTypesScenarioDataStore.MiddlewareTypesWrapper.Build();
            _middlewareTypesScenarioDataStore.MiddlewaresProvider = middlewaresReadOnlyCollection;
        }

        [Then(@"This middleware is created successfully")]
        public void ThenThisMiddlewareIsCreatedSuccessfully()
        {
            _middlewareTypesScenarioDataStore
                .MiddlewaresProvider
                .Middlewares
                .Should()
                .ContainSingle(t => t is CreatableMiddleware);
        }

        [Then(@"This middleware fails to create")]
        public void ThenThisMiddlewareFailsToCreate()
        {
            _middlewareTypesScenarioDataStore
                .MiddlewareTypesWrapper
                .Errors
                .Should()
                .ContainSingle();
        }
    }
}