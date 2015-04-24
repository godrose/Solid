using System;
using Solid.Practices.IoC;

namespace Solid.Practices.Composition.Desktop
{
    public class BootstrapperInitializationFacade<TIocContainer> : BootstrapperInitializationFacadeBase<TIocContainer> where TIocContainer : IIocContainer
    {
        private readonly Type _entryType;        

        public BootstrapperInitializationFacade(Type entryType, TIocContainer iocContainer) : base(iocContainer)
        {
            _entryType = entryType;
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(_entryType, CompositionContainer);
        }
    }
}
