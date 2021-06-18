using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.Extensibility;
using Solid.Tests.Infra.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class ExtensibilityByTypeSteps : StepsBase<FakeBootstrapperWithExtensibilityByType>
    {
        public ExtensibilityByTypeSteps(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer):
            base(scenarioContext, objectContainer,
                () => new FakeBootstrapperWithExtensibilityByType())
        {
        }

        [Given(@"The creatable middleware can be created")]
        public void GivenTheCreatableMiddlewareCanBeCreated()
        {
            CommonScenarioDataStore.IocContainer
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
            CommonScenarioDataStore.Object.UseAspect(
                new ExtensibilityByTypeAspect<FakeBootstrapperWithExtensibilityByType>(
                    CommonScenarioDataStore.Object,
                    CommonScenarioDataStore.IocContainer));
        }

        [When(@"The creatable middleware is used by type")]
        public void WhenTheCreatableMiddlewareIsUsedByType()
        {
            CommonScenarioDataStore.Object.Use<CreatableMiddleware>();
        }

        [When(@"The bootstrapper is initialized")]
        public void WhenTheBootstrapperIsInitialized()
        {
            CommonScenarioDataStore.Object.Initialize();
        }

        [Then(@"The creatable middleware is created")]
        public void ThenTheCreatableMiddlewareIsCreated()
        {
            var dependency = CommonScenarioDataStore.IocContainer.Resolve<ICreatableMiddlewareDependency>();
            dependency.IsCalled.Should().BeTrue();
        }
    }
}