using System.Collections.Generic;

namespace Solid.Extensibility
{
    public interface IAspectsProvider
    {
        /// <summary>
        /// Gets collection of aspects.
        /// </summary>
        IEnumerable<IAspect> Aspects { get; }
    }
}