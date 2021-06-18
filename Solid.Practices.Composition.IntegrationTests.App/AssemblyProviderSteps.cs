using System.Linq;
using FluentAssertions;
using Solid.Common;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class AssemblyProviderSteps
    {
        private readonly AssemblyProviderScenarioDataStore _scenarioDataStore;

        public AssemblyProviderSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new AssemblyProviderScenarioDataStore(scenarioContext);
        }

        [When(@"The assemblies provider loads the assemblies in the current folder")]
        public void WhenTheAssembliesProviderLoadsTheAssembliesInTheCurrentFolder()
        {
            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(), null,
                new[] { "Solid.Practices.Composition.IntegrationTests" });
            _scenarioDataStore.AssemblySourceProvider = assembliesProvider;
        }

        [Then(@"The loaded implementation type implements the loaded contract type")]
        public void ThenTheLoadedImplementationTypeImplementsTheLoadedContractType()
        {
            var assembliesProvider = _scenarioDataStore.AssemblySourceProvider;
            var assemblies = assembliesProvider.Assemblies.ToArray();

            var contractsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Contracts"));
            var placeHolderContract =
                contractsAssembly?.DefinedTypes.FirstOrDefault(t => t.Name == "IPlaceholder")?.AsType();
            var implementationsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Lib"));
            var placeHolderImplementation =
                implementationsAssembly?.DefinedTypes.FirstOrDefault(t => t.Name == "FakePlaceholder");

            placeHolderImplementation?.ImplementedInterfaces.Contains(placeHolderContract).Should().BeTrue("Implementation type should implement the contract type");
        }
    }
}
