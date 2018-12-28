using System;
using System.Collections.Generic;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The modularity information
    /// </summary>
    public struct ModularityInfo
    {
        /// <summary>
        /// Gets the collection of <see cref="T:Solid.Practices.Modularity.ICompositionModule" />.
        /// </summary>
        public IEnumerable<ICompositionModule> Modules { get; internal set; }

        /// <summary>Gets the collection of composition errors.</summary>
        public IEnumerable<Exception> Errors { get; internal set; }
    }
}