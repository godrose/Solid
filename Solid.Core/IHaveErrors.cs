using System;
using System.Collections.Generic;

namespace Solid.Core
{
    /// <summary>
    /// Gets the collection of errors that happen during initialization.
    /// </summary>
    public interface IHaveErrors
    {
        /// <summary>
        /// The errors.
        /// </summary>
        IEnumerable<Exception> Errors { get; }
    }
}