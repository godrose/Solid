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
            var options = table.CreateInstance<ContainerCreationData>();
            var prefixes = string.IsNullOrWhiteSpace(options.Prefixes)
                ? new string[] { }
                : options.Prefixes.Split(new[] {';'}).ToArray();
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<ICompositionModule> compositionContainer = new CompositionContainer<ICompositionModule>(new ActivatorCreationStrategy(),
                new FileSystemBasedAssemblyLoadingStrategy(rootPath, prefixes: prefixes, namespaces: null, extensions: AssemblyLoadingManager.Extensions().ToArray()));
            _scenarioContext.Add("compositionContainer", compositionContainer);
        }

        [When(@"The composition container for custom modules is created in the current folder")]
        public void WhenTheCompositionContainerForCustomModulesIsCreatedInTheCurrentFolder(Table table)
        {
            var options = table.CreateInstance<ContainerCreationData>();
            var prefixes = string.IsNullOrWhiteSpace(options.Prefixes)
                ? new string[] { }
                : options.Prefixes.Split(new[] { ';' }).ToArray();
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(new ActivatorCreationStrategy(),
                new FileSystemBasedAssemblyLoadingStrategy(rootPath, prefixes: prefixes, namespaces: null, extensions: AssemblyLoadingManager.Extensions().ToArray()));
            _scenarioContext.Add("compositionContainer", compositionContainer);
        }

        [When(@"The composition container for other modules is created in the current folder")]
        public void WhenTheCompositionContainerForOtherModulesIsCreatedInTheCurrentFolder(Table table)
        {
            var options = table.CreateInstance<ContainerCreationData>();
            var prefixes = string.IsNullOrWhiteSpace(options.Prefixes)
                ? new string[] { }
                : options.Prefixes.Split(new[] { ';' }).ToArray();
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<IAnotherModule> compositionContainer = new CompositionContainer<IAnotherModule>(new ActivatorCreationStrategy(),
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
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<ICompositionModule>>(
                    "compositionContainer");
            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(expectedCount);
        }

        [Then(@"There should be (.*) custom modules")]
        public void ThenThereShouldBeCustomModules(int expectedCount)
        {
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<ICustomModule>>(
                    "compositionContainer");
            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(expectedCount);
        }

        [Then(@"There should be (.*) other modules")]
        public void ThenThereShouldBeOtherModules(int expectedCount)
        {
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<IAnotherModule>>(
                    "compositionContainer");
            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(expectedCount);
        }
    }

    public sealed class ContainerCreationData
    {
        public string Prefixes { get; set; }
    }
}
