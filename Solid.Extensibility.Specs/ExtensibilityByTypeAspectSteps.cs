using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.Tests.Infra.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeAspectSteps : StepsBase<ExtensibleByTypeObject>
    {
        private readonly ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject> _aspectScenarioDataStore;

        public ExtensibilityByTypeAspectSteps(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer):
            base(
                scenarioContext, 
                objectContainer, 
                () =>  new ExtensibleByTypeObject())
        {
            _aspectScenarioDataStore =
                new ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject>(scenarioContext);
        }

        [When(@"The extensibility by type aspect is created")]
        public void WhenTheExtensibilityByTypeAspectIsCreated()
        {
            _aspectScenarioDataStore.Aspect =
                new ExtensibilityByTypeAspect<ExtensibleByTypeObject>(
                    CommonScenarioDataStore.RootObject,
                    CommonScenarioDataStore.IocContainer);
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
            var dependency = CommonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeTrue();
        }

        [Then(@"The creatable middleware is not executed")]
        public void ThenTheCreatableMiddlewareIsNotExecuted()
        {
            var dependency = CommonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeFalse();
        }
    }
}