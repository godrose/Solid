using Solid.Core;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an aspect which is used during initialization
    /// </summary>
    public interface IAspect : IInitializable, IHaveDependencies
    {
        /// <summary>
        /// Aspect id.
        /// </summary>
        string Id { get; }
    }
}