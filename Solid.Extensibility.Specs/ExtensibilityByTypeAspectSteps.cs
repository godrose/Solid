using Attest.Testing.SpecFlow;
using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeAspectSteps
    {
        private readonly ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject> _aspectScenarioDataStore;
        private readonly CommonScenarioDataStore<ExtensibleByTypeObject> _commonScenarioDataStore;

        public ExtensibilityByTypeAspectSteps(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer)
        {
            _commonScenarioDataStore =
                CommonScenarioDataStoreFactory.CreateCommonScenarioDataStore(
                    scenarioContext,
                    objectContainer,
                    () => new ExtensibleByTypeObject());
            _aspectScenarioDataStore =
                new ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject>(scenarioContext);
        }

        [When(@"The extensibility by type aspect is created")]
        public void WhenTheExtensibilityByTypeAspectIsCreated()
        {
            _aspectScenarioDataStore.Aspect =
                new ExtensibilityByTypeAspect<ExtensibleByTypeObject>(
                    _commonScenarioDataStore.Object,
                    _commonScenarioDataStore.IocContainer);
        }

        [When(@"The creatable middleware is used by the aspect")]
        public void WhenTheCreatableMiddlewareIsUsedByTheAspect()
        {
            _aspectScenarioDataStore.Aspect.Use<CreatableMiddleware>();
        }

        [When(@"The extensibility by type aspect is initialized")]
        public void WhenTheExtensibilityByTypeAspectIsInitialized()
        {
            _aspectScenarioDataStore.Aspect.Initialize();
        }

        [Then(@"The creatable middleware is executed")]
        public void ThenTheCreatableMiddlewareIsExecuted()
        {
            var dependency = _commonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeTrue();
        }

        [Then(@"The creatable middleware is not executed")]
        public void ThenTheCreatableMiddlewareIsNotExecuted()
        {
            var dependency = _commonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeFalse();
        }
    }
}