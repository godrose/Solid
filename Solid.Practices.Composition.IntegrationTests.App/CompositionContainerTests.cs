using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BoDi;
using FluentAssertions;
using McMaster.NETCore.Plugins;
using Solid.Common;
using Solid.IoC.Adapters.BoDi;
using Solid.Practices.Composition.Contracts;
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
            AssemblyLoader.LoadAssembliesFromPaths = Loader.Get;
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
                        
        }
    }


    class Loader
    {
        public static IEnumerable<Assembly> Get(IEnumerable<string> paths)
        {
            return paths.Select(path =>
                PluginLoader.CreateFromAssemblyFile(assemblyFile: Path.Combine(Directory.GetCurrentDirectory(), path),
                    sharedTypes: new[] {typeof(ICompositionModule<IDependencyRegistrator>)}).LoadDefaultAssembly());
        }
    }
}
