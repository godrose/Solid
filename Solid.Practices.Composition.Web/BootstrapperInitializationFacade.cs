using Solid.Practices.IoC;

namespace Solid.Practices.Composition.Web
{
    public class BootstrapperInitializationFacade : BootstrapperInitializationFacadeBase
    {
        public BootstrapperInitializationFacade(IIocContainer iocContainer) : base(iocContainer)
        {
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(CompositionContainer);
        }
    }
}
