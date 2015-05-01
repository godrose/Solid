using System.Collections.Generic;

namespace Solid.Practices.Composition
{
    public interface IBootstrapperInitializationFacade
    {
        IAssembliesReadOnlyResolver AssembliesResolver { get; }
        IEnumerable<ICompositionModule> Modules { get; }

        void Initialize(string rootPath);
    }
}
