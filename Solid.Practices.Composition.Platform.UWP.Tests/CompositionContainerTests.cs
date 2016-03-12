using System.Linq;
using NUnit.Framework;

namespace Solid.Practices.Composition.Platform.UWP.Tests
{    
    [TestFixture]
    class CompositionContainerTests
    {
        [Test]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer compositionContainer = new CompositionContainer(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(1, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModules_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndCorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Solid" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndIncorrectPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath, new[] { "Incorrect" });
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(0, modulesCount);
        }

        [Test]
        public void RootPathContainsCustomModulesAndNoPrefixIsUsed_CustomModulesAreImported()
        {
            var rootPath = GetCurrentDirectory();

            ICompositionContainer<ICustomModule> compositionContainer = new CompositionContainer<ICustomModule>(rootPath);
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var modulesCount = modules.Count();
            Assert.AreEqual(2, modulesCount);
        }

        //TODO: Removed the hardcoded path
        private static string GetCurrentDirectory()
        {
#if DEBUG
            return @"C:\Workspace\Solid\Solid.Practices.Composition.Platform.UWP.Tests\bin\Debug";
#endif
            return @"C:\Workspace\Solid\Solid.Practices.Composition.Platform.UWP.Tests\bin\Release";
        }
    }
}
