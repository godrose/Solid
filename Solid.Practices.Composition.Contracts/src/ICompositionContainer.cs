using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents strongly-typed composition container.
    /// </summary>
    public interface ICompositionContainer : ICompositionContainer<ICompositionModule>, ICompositionModulesProvider
    {

    }

    /// <summary>
    /// Represents composition container which allows composing the composition modules.
    /// </summary>
    /// <typeparam name="TModule">Type of composition module.</typeparam>
    public interface ICompositionContainer<TModule> : ICompositionModulesProvider<TModule>
    {
        /// <summary>
        /// Composes the composition modules.
        /// </summary>
        void Compose();
    }
}
