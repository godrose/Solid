using System.IO;
using System.Linq;
using FluentAssertions;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Practices.Composition.Platform.UWP.Tests
{           
    public class CompositionContainerTests
    {
        static CompositionContainerTests()
        {
            PlatformProvider.Current = new UniversalPlatformProvider();
        }

        [Fact]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICompositionModule> compositionContainer = CreateCompositionContainer<ICompositionModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(1);
        }

        [Fact]
        public void RootPathContainsCustomModules_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(2);
        }

        [Fact]
        public void RootPathContainsCustomModulesAndCorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath, new[] { "Solid" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(2);
        }

        [Fact]
        public void RootPathContainsCustomModulesAndIncorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath, new[] { "Incorrect" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(0);
        }

        [Fact]
        public void RootPathContainsCustomModulesAndNoPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            modulesCount.Should().Be(2);
        }

        //TODO: Removed the hardcoded path
        private static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        private static CompositionContainer<TModule> CreateCompositionContainer<TModule>(string rootPath)
            where TModule : ICompositionModule
        {
            return new CompositionContainer<TModule>(CreateModuleCreationStrategy(), rootPath);
        }

        private static CompositionContainer<TModule> CreateCompositionContainer<TModule>(string rootPath, string[] prefixes)
            where TModule : ICompositionModule
        {
            return new CompositionContainer<TModule>(CreateModuleCreationStrategy(), rootPath, prefixes);
        }

        private static ICompositionModuleCreationStrategy CreateModuleCreationStrategy()
        {
            return new ActivatorCreationStrategy();            
        }
    }
}
