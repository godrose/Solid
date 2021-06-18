using Attest.Testing.SpecFlow;
using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.Extensibility;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeSteps
    {
        private readonly CommonScenarioDataStore<FakeBootstrapperWithExtensibilityByType> 
            _commonScenarioDataStore;

        public ExtensibilityByTypeSteps(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer)
        {
            _commonScenarioDataStore =
                CommonScenarioDataStoreFactory.CreateCommonScenarioDataStore(
                    scenarioContext, 
                    objectContainer, 
                    () => new FakeBootstrapperWithExtensibilityByType());
        }

        [Given(@"The creatable middleware can be created")]
        public void GivenTheCreatableMiddlewareCanBeCreated()
        {
            _commonScenarioDataStore.IocContainer
                .RegisterSingleton<ICreatableMiddlewareDependency, CreatableMiddlewareDependency>();
        }

        [When(@"The new bootstrapper with support for extensibility by type is created")]
        public void WhenTheNewBootstrapperWithSupportForExtensibilityByTypeIsCreated()
        {
            //for readability only
        }

        [When(@"The extensibility by type aspect is used")]
        public void WhenTheExtensibilityByTypeAspectIsUsed()
        {
            _commonScenarioDataStore.Object.UseAspect(
                new ExtensibilityByTypeAspect<FakeBootstrapperWithExtensibilityByType>(
                    _commonScenarioDataStore.Object,
                    _commonScenarioDataStore.IocContainer));
        }

        [When(@"The creatable middleware is used by type")]
        public void WhenTheCreatableMiddlewareIsUsedByType()
        {
            _commonScenarioDataStore.Object.Use<CreatableMiddleware>();
        }

        [When(@"The bootstrapper is initialized")]
        public void WhenTheBootstrapperIsInitialized()
        {
            _commonScenarioDataStore.Object.Initialize();
        }

        [Then(@"The creatable middleware is created")]
        public void ThenTheCreatableMiddlewareIsCreated()
        {
            var dependency = _commonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeTrue();
        }
    }
}