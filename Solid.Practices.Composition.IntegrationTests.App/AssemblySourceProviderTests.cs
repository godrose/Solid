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

        [Fact]
        public void ResolveContract_AssembliesAreLoadedUsingCustomLoader_ImplementationsAreRegistered()
        {
            AssemblyLoader.LoadAssembliesFromPaths = Loader.Get;

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
