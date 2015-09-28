using Solid.Practices.IoC;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Bootstrapper initialization facade for server part of web applications
    /// </summary>
    /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
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
