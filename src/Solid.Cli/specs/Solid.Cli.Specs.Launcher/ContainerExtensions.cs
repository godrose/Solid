using Solid.Practices.IoC;

namespace Solid.Cli.Specs.Launcher
{
    public static class ContainerExtensions
    {
        public static void Initialize(this IIocContainer iocContainer) =>
            new Startup(iocContainer).Initialize();
    }
}
