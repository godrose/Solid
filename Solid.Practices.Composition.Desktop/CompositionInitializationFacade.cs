using System;

namespace Solid.Practices.Composition.Desktop
{
    /// <summary>
    /// Composition initialization facade for client part of desktop applications
    /// </summary>
    public class CompositionInitializationFacade : CompositionInitializationFacadeBase
    {
        private readonly Type _entryType;        

        public CompositionInitializationFacade(Type entryType)
        {
            _entryType = entryType;
        }

        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(_entryType, CompositionContainer);
        }
    }
}
