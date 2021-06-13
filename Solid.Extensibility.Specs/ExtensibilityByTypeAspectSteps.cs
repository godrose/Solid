using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.IoC.Adapters.BoDi;
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
            _commonScenarioDataStore = new CommonScenarioDataStore<ExtensibleByTypeObject>(scenarioContext);
            _commonScenarioDataStore.Object = new ExtensibleByTypeObject();
            var containerAdapter = new ObjectContainerAdapter(objectContainer);
            _commonScenarioDataStore.IocContainer = containerAdapter;
            _aspectScenarioDataStore =
                new ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject>(scenarioContext);
        }

        [When(@"The extensibility by type aspect is created")]
        public void WhenTheExtensibilityByTypeAspectIsCreated()
        {
            _aspectScenarioDataStore.Aspect =
                new ExtensibilityByTypeAspect<ExtensibleByTypeObject>(_commonScenarioDataStore.Object,
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