using System.Linq;
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.WIN81.Tests
{
    [TestClass]
    public class CompositionContainerTests
    {        
        [TestInitialize]
        public void OneTimeSetUp()
        {           
            PlatformProvider.Current = new Win81PlatformProvider();
        }

        [TestMethod]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();
            
            ICompositionContainer compositionContainer = new CompositionContainer(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(3, modulesCount);
        }        

        [TestMethod]
        public void RootPathContainsCustomModules_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndCorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Solid" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndIncorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Incorrect" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(0, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsCustomModulesAndNoPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [TestMethod]
        public void RootPathContainsOtherModules_OtherModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<IAnotherModule> compositionContainer = new CompositionContainer<IAnotherModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(1, modulesCount);
        }

        //TODO: Removed the hardcoded path
        private static string GetCurrentDirectory()
        {
#if DEBUG
            return @"C:\Workspace\Solid\Solid.Practices.Composition.WIN81.Tests\bin\Debug\AppX";
#else
            return @"C:\Workspace\Solid\Solid.Practices.Composition.WIN81.Tests\bin\Release\AppX";
#endif
        }
    }

}
