using System.Collections.Generic;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    public interface IMiddlewaresReadOnlyCollection<TExtensible> where TExtensible : class
    {
        /// <summary>
        /// Gets collection of middlewares.
        /// </summary>
        IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}