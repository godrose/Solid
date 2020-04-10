using System.IO;
using System.Linq;
using BoDi;
using FluentAssertions;
using Solid.IoC.Adapters.BoDi;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Solid.Practices.Composition.Tests.Steps.Adapters
{
    [Binding]
    internal sealed class CompositionContainerStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public CompositionContainerStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"The composition container is created in the current folder")]
        public void WhenTheCompositionContainerIsCreatedInTheCurrentFolder()
        {
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<ICompositionModule<IDependencyRegistrator>> compositionContainer = new CompositionContainer<ICompositionModule<IDependencyRegistrator>>(new ActivatorCreationStrategy(),
                new FileSystemBasedAssemblyLoadingStrategy(rootPath, prefixes: new[] { "Solid" }, namespaces: null, extensions: AssemblyLoadingManager.Extensions().ToArray()));
            _scenarioContext.Add("compositionContainer", compositionContainer);
        }

        [When(@"The composition container for composition modules is created in the current folder")]
        public void WhenTheCompositionContainerForCompositionModulesIsCreatedInTheCurrentFolder(Table table)
        {
            ComposeContainer<ICompositionModule>(table);
        }

        [When(@"The composition container for custom modules is created in the current folder")]
        public void WhenTheCompositionContainerForCustomModulesIsCreatedInTheCurrentFolder(Table table)
        {
            ComposeContainer<ICustomModule>(table);
        }

        [When(@"The composition container for other modules is created in the current folder")]
        public void WhenTheCompositionContainerForOtherModulesIsCreatedInTheCurrentFolder(Table table)
        {
            ComposeContainer<IAnotherModule>(table);
        }

        private void ComposeContainer<TModule>(Table table) where TModule : ICompositionModule
        {
            var options = table.CreateInstance<ContainerCreationData>();
            var prefixes = string.IsNullOrWhiteSpace(options.Prefixes)
                ? new string[] { }
                : options.Prefixes.Split(new[] { ';' }).ToArray();
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<TModule> compositionContainer = new CompositionContainer<TModule>(new ActivatorCreationStrategy(),
                new FileSystemBasedAssemblyLoadingStrategy(rootPath, prefixes: prefixes, namespaces: null, extensions: AssemblyLoadingManager.Extensions().ToArray()));
            _scenarioContext.Add("compositionContainer", compositionContainer);
        }

        [When(@"The composition container is composed")]
        public void WhenTheCompositionContainerIsComposed()
        {
            var compositionContainer =
                _scenarioContext.Get<IComposer>(
                    "compositionContainer");
            compositionContainer?.Compose();
        }

        [When(@"The single composition module is registered")]
        public void WhenTheSingleCompositionModuleIsRegistered()
        {
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<ICompositionModule<IDependencyRegistrator>>>(
                    "compositionContainer");
            var modules = compositionContainer.Modules;
            var registrator = new ObjectContainerAdapter(new ObjectContainer());
            var singleModule = modules.SingleOrDefault();
            singleModule.RegisterModule(registrator);
            _scenarioContext.Add("resolver", registrator);
        }

        [Then(@"There should be (.*) composition modules")]
        public void ThenThereShouldBeCompositionModules(int expectedCount)
        {
            AssertModulesCount<ICompositionModule>(expectedCount);
        }

        [Then(@"There should be (.*) custom modules")]
        public void ThenThereShouldBeCustomModules(int expectedCount)
        {
            AssertModulesCount<ICustomModule>(expectedCount);
        }

        [Then(@"There should be (.*) other modules")]
        public void ThenThereShouldBeOtherModules(int expectedCount)
        {
            AssertModulesCount<IAnotherModule>(expectedCount);
        }

        private void AssertModulesCount<TModule>(int expectedCount)
        {
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<TModule>>(
                    "compositionContainer");
            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(expectedCount);
        }
    }
}
