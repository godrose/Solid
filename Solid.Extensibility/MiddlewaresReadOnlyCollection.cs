using System.Collections.Generic;
using System.Linq;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    public class MiddlewaresReadOnlyCollection<TExtensible> : IMiddlewaresReadOnlyCollection<TExtensible>
        where TExtensible : class
    {
        public MiddlewaresReadOnlyCollection(IEnumerable<IMiddleware<TExtensible>> middlewares)
        {
            Middlewares = middlewares.ToArray();
        }

        public IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}