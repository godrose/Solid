using Attest.Testing.Bootstrapping;
using Solid.Practices.IoC;

namespace Solid.Extensibility.Specs
{
    internal sealed class Startup : StartupBase<Bootstrapper>
    {
        public Startup(IIocContainer iocContainer)
            : base(iocContainer, c => new Bootstrapper(c))
        {
        }
    }
}