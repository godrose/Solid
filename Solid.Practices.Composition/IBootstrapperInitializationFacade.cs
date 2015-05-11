using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    public interface IBootstrapperInitializationFacade
    {
        IAssembliesReadOnlyResolver AssembliesResolver { get; }
        IEnumerable<ICompositionModule> Modules { get; }

        void Initialize(string rootPath);
    }
}
