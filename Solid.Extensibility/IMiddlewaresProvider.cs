using System.Collections.Generic;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an extensible object which contains middlewares.
    /// </summary>
    /// <typeparam name="TExtensible">The type of the extensible object.</typeparam>
    public interface IMiddlewaresProvider<TExtensible> where TExtensible : class
    {
        /// <summary>
        /// Gets collection of middlewares.
        /// </summary>
        IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}