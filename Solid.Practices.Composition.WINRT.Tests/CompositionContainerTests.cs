using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.WINRT.Tests
{
    [TestClass]
    public class CompositionContainerTests
    {        
        [TestInitialize]
        public void OneTimeSetUp()
        {           
            PlatformProvider.Current = new WinRTPlatformProvider();
        }

        [TestMethod]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();
            
            ICompositionContainer<ICompositionModule> compositionContainer = CreateCompositionContainer<ICompositionModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(3, modulesCount);
        }        

        [TestMethod]
        public void RootPathContainsCustomModules_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndCorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath, new[] { "Solid" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndIncorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath, new[] { "Incorrect" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(0, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndNoPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = CreateCompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsOtherModules_OtherModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<IAnotherModule> compositionContainer = CreateCompositionContainer<IAnotherModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(1, modulesCount);
        }

        //TODO: Removed the hardcoded path
        private static string GetCurrentDirectory()
        {
            
#if DEBUG
            return @"C:\Workspace\Solid\Solid.Practices.Composition.WINRT.Tests\bin\Debug\AppX";
#else
            return @"C:\Workspace\Solid\Solid.Practices.Composition.WINRT.Tests\bin\Release\AppX";
#endif
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
