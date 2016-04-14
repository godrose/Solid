using System;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents module creation strategy.
    /// </summary>
    public interface ICompositionModuleCreationStrategy
    {
        /// <summary>
        /// Creates composition module from its type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object CreateCompositionModule(Type type);
    }
}
