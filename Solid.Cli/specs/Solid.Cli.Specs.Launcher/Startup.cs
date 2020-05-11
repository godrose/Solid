using Attest.Testing.Core;
using Common.Bootstrapping;
using Solid.Bootstrapping;
using Solid.Core;
using Solid.Practices.IoC;
using BootstrapperBase = Attest.Testing.Bootstrapping.BootstrapperBase;

namespace Solid.Cli.Specs.Launcher
{
    internal sealed class Startup : IInitializable
    {
        private readonly IIocContainer _iocContainer;

        public Startup(IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public void Initialize()
        {
            var bootstrapper = new Bootstrapper(_iocContainer);
            bootstrapper.UseDynamicLoad();
            bootstrapper
                .Use(new RegisterCustomCompositionModulesMiddleware<BootstrapperBase, IDependencyRegistrator>())
                .Use(new RegisterResolverMiddleware<BootstrapperBase>(_iocContainer))
                .Use(new UseLifecycleMiddleware<BootstrapperBase>());
            bootstrapper.Initialize();
        }
    }
}
