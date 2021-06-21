using System;
using System.Collections.Generic;

namespace Solid.Core
{
    /// <summary>
    /// Gets the collection of errors that happened during initialization/build process.
    /// </summary>
    public interface IHaveErrors
    {
        /// <summary>
        /// The collection of errors.
        /// </summary>
        IEnumerable<Exception> Errors { get; }
    }
}