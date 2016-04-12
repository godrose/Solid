using System;

namespace Solid.Practices.Composition.Contracts
{
    /// <summary>
    /// Represents module creation strategy.
    /// </summary>
    public interface IModuleCreationStrategy
    {
        /// <summary>
        /// Creates module from its type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object CreateModule(Type type);
    }
}
