using System.Reflection;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;

namespace Solid.Bootstrapping.Specs
{
    [Binding]
    internal sealed class BootstrapperStepsAdapter
    {
        //TODO: UseContainer
        private readonly ScenarioContext _scenarioContext;

        public BootstrapperStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"The new bootstrapper with composition modules is created")]
        public void WhenTheNewBootstrapperWithCompositionModulesIsCreated()
        {
            var container = _scenarioContext.Get<IDependencyRegistrator>("container");

            var bootstrapper = new FakeBootstrapper
            {
                Registrator = container,
                Modules = new ICompositionModule[]
                {
                    new TransientIocCompositionModule()
                }
            };
            _scenarioContext.Add("bootstrapper", bootstrapper);
        }

        [When(@"The new bootstrapper with current assembly is created")]
        public void WhenTheNewBootstrapperWithCurrentAssemblyIsCreated()
        {
            var container = _scenarioContext.Get<IDependencyRegistrator>("container");

            var bootstrapper = new FakeBootstrapper
            {
                Registrator = container,
                Assemblies = new[] { typeof(FakeIocContainer).GetTypeInfo().Assembly }
            };
            _scenarioContext.Add("bootstrapper", bootstrapper);
        }

        [When(@"The composition modules middleware is applied onto the bootstrapper")]
        public void WhenTheCompositionModulesMiddlewareIsAppliedOntoTheBootstrapper()
        {
            var bootstrapper = _scenarioContext.Get<FakeBootstrapper>("bootstrapper");
            var middleware = new RegisterCompositionModulesMiddleware<FakeBootstrapper>();
            middleware.Apply(bootstrapper);
        }

        [When(@"The collection registration middleware is applied onto the bootstrapper")]
        public void WhenTheCollectionRegistrationMiddlewareIsAppliedOntoTheBootstrapper()
        {
            var bootstrapper = _scenarioContext.Get<FakeBootstrapper>("bootstrapper");
            var middleware = new RegisterCollectionMiddleware<FakeBootstrapper>(typeof(IServiceContract));
            middleware.Apply(bootstrapper);
        }
    }
}
