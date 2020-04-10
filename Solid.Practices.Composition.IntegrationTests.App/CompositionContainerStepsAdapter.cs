using System.IO;
using System.Linq;
using BoDi;
using FluentAssertions;
using Solid.IoC.Adapters.BoDi;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Composition.IntegrationTests.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
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

        [When(@"The composition container is composed")]
        public void WhenTheCompositionContainerIsComposed()
        {
            var compositionContainer =
                _scenarioContext.Get<ICompositionContainer<ICompositionModule<IDependencyRegistrator>>>(
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

        [Then(@"The loaded placeholder type is OK")]
        public void ThenTheLoadedPlaceholderTypeIsOK()
        {
            var resolver = _scenarioContext.Get<IDependencyResolver>("resolver");
            var placeholder = resolver.Resolve<IPlaceholder>();
            var length = placeholder.Length;
            length.Should().Be(5);
        }

    }
}
