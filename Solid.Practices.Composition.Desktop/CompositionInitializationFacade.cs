using System;

namespace Solid.Practices.Composition.Desktop
{
    /// <summary>
    /// Composition initialization facade for client part of desktop applications
    /// </summary>
    public class CompositionInitializationFacade : CompositionInitializationFacadeBase
    {
        private readonly Type _entryType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionInitializationFacade"/> class.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        public CompositionInitializationFacade(Type entryType)
        {
            _entryType = entryType;
        }

        /// <summary>
        /// Creates the assemblies resolver.
        /// </summary>
        /// <returns></returns>
        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(_entryType, CompositionContainer);
        }
    }
}
