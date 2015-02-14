using System;
using Solid.Practices.IoC;

namespace Solid.Practices.Composition.Desktop
{
    public class BootstrapperInitializationFacade : BootstrapperInitializationFacadeBase
    {
        private readonly Type _entryType;        

        public BootstrapperInitializationFacade(Type entryType, IIocContainer iocContainer) : base(iocContainer)
        {
            _entryType = entryType;
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(_entryType, CompositionContainer);
        }
    }
}
