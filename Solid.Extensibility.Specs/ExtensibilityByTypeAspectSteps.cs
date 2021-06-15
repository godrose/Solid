using FluentAssertions;
using JetBrains.Annotations;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeAspectSteps
    {
        private readonly IIocContainer _iocContainer;
        private readonly ExtensibleByTypeObject _object;
        private readonly ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject> _aspectScenarioDataStore;

        public ExtensibilityByTypeAspectSteps(
            IIocContainer iocContainer,
            ExtensibleByTypeObject @object,
            ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject> aspectScenarioDataStore)
        {
            _iocContainer = iocContainer;
            _object = @object;
            _aspectScenarioDataStore = aspectScenarioDataStore;
        }

        [When(@"The extensibility by type aspect is created")]
        public void WhenTheExtensibilityByTypeAspectIsCreated()
        {
            _aspectScenarioDataStore.Aspect =
                new ExtensibilityByTypeAspect<ExtensibleByTypeObject>(_object,
                   _iocContainer);
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
            var dependency = _iocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeTrue();
        }

        [Then(@"The creatable middleware is not executed")]
        public void ThenTheCreatableMiddlewareIsNotExecuted()
        {
            var dependency = _iocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeFalse();
        }
    }
}