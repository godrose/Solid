using System.Reflection;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [Binding]
    internal sealed class BootstrapperSteps
    {
        private readonly BootstrapperScenarioDataStore _bootstrapperScenarioDataStore;

        public BootstrapperSteps(ScenarioContext scenarioContext)
        {
            _bootstrapperScenarioDataStore = new BootstrapperScenarioDataStore(scenarioContext);
        }

        [When(@"The new bootstrapper with composition modules is created")]
        public void WhenTheNewBootstrapperWithCompositionModulesIsCreated()
        {
            var container = _bootstrapperScenarioDataStore.Container;

            var bootstrapper = new FakeBootstrapper
            {
                Registrator = container,
                Modules = new ICompositionModule[]
                {
                    new TransientIocCompositionModule()
                }
            };
            _bootstrapperScenarioDataStore.Bootstrapper = bootstrapper;
        }

        [When(@"The new bootstrapper with current assembly is created")]
        public void WhenTheNewBootstrapperWithCurrentAssemblyIsCreated()
        {
            var container = _bootstrapperScenarioDataStore.Container;

            var bootstrapper = new FakeBootstrapper
            {
                Registrator = container,
                Assemblies = new[] { typeof(FakeIocContainer).GetTypeInfo().Assembly }
            };
            _bootstrapperScenarioDataStore.Bootstrapper = bootstrapper;
        }

        [When(@"The composition modules middleware is applied onto the bootstrapper")]
        public void WhenTheCompositionModulesMiddlewareIsAppliedOntoTheBootstrapper()
        {
            var bootstrapper = _bootstrapperScenarioDataStore.Bootstrapper;
            var middleware = new RegisterCompositionModulesMiddleware<FakeBootstrapper>();
            middleware.Apply(bootstrapper);
        }

        [When(@"The collection registration middleware is applied onto the bootstrapper")]
        public void WhenTheCollectionRegistrationMiddlewareIsAppliedOntoTheBootstrapper()
        {
            var bootstrapper = _bootstrapperScenarioDataStore.Bootstrapper;
            var middleware = new RegisterCollectionMiddleware<FakeBootstrapper>(typeof(IServiceContract));
            middleware.Apply(bootstrapper);
        }
    }
}
