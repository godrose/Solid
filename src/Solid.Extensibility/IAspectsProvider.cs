using System.Collections.Generic;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an object which contains aspects.
    /// </summary>
    public interface IAspectsProvider
    {
        /// <summary>
        /// Gets collection of aspects.
        /// </summary>
        IEnumerable<IAspect> Aspects { get; }
    }
}