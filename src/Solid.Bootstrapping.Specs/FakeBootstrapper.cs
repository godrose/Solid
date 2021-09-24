using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping.Specs
{
    internal class FakeBootstrapper : ICompositionModulesProvider, IAssemblySourceProvider, IHaveRegistrator
    {
        public IEnumerable<Assembly> Assemblies { get; internal set; }
        public IEnumerable<ICompositionModule> Modules { get; internal set; }
        public IDependencyRegistrator Registrator { get; internal set; }
    }

    internal class FakeBootstrapperWithExtensibilityByType :
        BootstrapperBase,
        IExtensibleByType<FakeBootstrapperWithExtensibilityByType>
    {
        public FakeBootstrapperWithExtensibilityByType Use<TExtension>()
            where TExtension : class, IMiddleware<FakeBootstrapperWithExtensibilityByType>
        {
            if (((IAspectsProvider) this).Aspects.FirstOrDefault(t =>
                    t is ExtensibilityByTypeAspect<FakeBootstrapperWithExtensibilityByType>) is
                ExtensibilityByTypeAspect<FakeBootstrapperWithExtensibilityByType> extensibilityByTypeAspect)
                extensibilityByTypeAspect.Use<TExtension>();

            return this;
        }
    }
}