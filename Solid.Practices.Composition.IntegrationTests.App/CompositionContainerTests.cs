using System.IO;
using System.Linq;
using BoDi;
using FluentAssertions;
using Solid.Common;
using Solid.IoC.Adapters.BoDi;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Composition.IntegrationTests.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    public class CompositionContainerTests
    {
        static CompositionContainerTests()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
            AssemblyLoader.LoadAssembliesFromPaths = DynamicLoader.LoadAssemblies;
        }

        [Fact]
        public void RootPathContainsCompositionModules_CompositionModulesAreImported()
        {
            var rootPath = Directory.GetCurrentDirectory();

            ICompositionContainer<ICompositionModule<IDependencyRegistrator>> compositionContainer = new CompositionContainer<ICompositionModule<IDependencyRegistrator>>(new ActivatorCreationStrategy(), 
                new FileSystemBasedAssemblyLoadingStrategy(rootPath, prefixes: new []{ "Solid" }, namespaces: null, extensions: AssemblyLoadingManager.Extensions().ToArray()));
            compositionContainer.Compose();

            var modules = compositionContainer.Modules;
            var registrator = new ObjectContainerAdapter(new ObjectContainer());
            var singleModule = modules.SingleOrDefault();
            singleModule.RegisterModule(registrator);

            var placeHolder = registrator.Resolve<IPlaceholder>();
            var length = placeHolder.Length;
            length.Should().Be(5);
        }
    }
}
