using JetBrains.Annotations;
using Solid.Cli.Specs.Tests.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Cli.Specs.Tests.Infra
{
    [UsedImplicitly]
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator
                .AddSingleton<IProcessManagementService, WindowsProcessManagementService>();
        }
    }
}
