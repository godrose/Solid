using Solid.Practices.IoC;

namespace Solid.Practices.Composition.Web
{
    public class BootstrapperInitializationFacade<TIocContainer> : BootstrapperInitializationFacadeBase<TIocContainer> 
        where TIocContainer : IIocContainer
    {
        public BootstrapperInitializationFacade(TIocContainer iocContainer)
            : base(iocContainer)
        {
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(CompositionContainer);
        }
    }
}
