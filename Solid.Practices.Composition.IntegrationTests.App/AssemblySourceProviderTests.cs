using System.Linq;
using FluentAssertions;
using Solid.Common;
using Xunit;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    public class AssemblySourceProviderTests
    {
        static AssemblySourceProviderTests()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        //TODO:Restore
        [Fact(Skip = "This test fails on purpose to demonstrate dynamic loading issue thus it's ignored for now")]
        public void ResolveContract_AssembliesAreLoadedUsingCustomLoader_ImplementationsAreRegistered()
        {
            AssemblyLoader.LoadAssembliesFromPaths = DynamicLoader.LoadAssemblies;

            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(), null,
                new[] { "Solid.Practices.Composition.IntegrationTests" });
            var assemblies = assembliesProvider.Assemblies;

            var contractsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Contracts"));
            var placeHolderContract =
                contractsAssembly.DefinedTypes.FirstOrDefault(t => t.Name == "IPlaceholder").AsType();
            var implementationsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Lib"));
            var placeHolderImplementation =
                implementationsAssembly.DefinedTypes.FirstOrDefault(t => t.Name == "FakePlaceholder");

            placeHolderImplementation.ImplementedInterfaces.Contains(placeHolderContract).Should().BeTrue();
        }

        [Fact]
        public void ResolveContract_AssembliesAreLoadedUsingRegularLoader_ImplementationsAreRegistered()
        {
            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(), null,
                new[] { "Solid.Practices.Composition.IntegrationTests" });
            var assemblies = assembliesProvider.Assemblies;

            var contractsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Contracts"));
            var placeHolderContract =
                contractsAssembly.DefinedTypes.FirstOrDefault(t => t.Name == "IPlaceholder").AsType();
            var implementationsAssembly = assemblies.FirstOrDefault(t => t.GetName().Name.EndsWith("Lib"));
            var placeHolderImplementation =
                implementationsAssembly.DefinedTypes.FirstOrDefault(t => t.Name == "FakePlaceholder");

            placeHolderImplementation.ImplementedInterfaces.Contains(placeHolderContract).Should().BeTrue();
        }
    }
}
