using System.Collections.Generic;
using System.Linq;
using Solid.Practices.Middleware;

namespace Solid.Extensibility.Specs
{
    internal class MiddlewaresReadOnlyCollection<TExtensible> where TExtensible : class
    {
        public MiddlewaresReadOnlyCollection(IEnumerable<IMiddleware<TExtensible>> middlewares)
        {
            Middlewares = middlewares.ToArray();
        }

        public IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}