using System.Collections.Generic;
using System.Linq;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <inheritdoc/>
    public class MiddlewaresProvider<TExtensible> : IMiddlewaresProvider<TExtensible>
        where TExtensible : class
    {
        /// <summary>
        /// Creates new instance of <see cref="MiddlewaresProvider{TExtensible}"/>
        /// </summary>
        /// <param name="middlewares">The collection of the middlewares.</param>
        public MiddlewaresProvider(IEnumerable<IMiddleware<TExtensible>> middlewares)
        {
            Middlewares = middlewares.ToArray();
        }

        /// <inheritdoc/>
        public IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}