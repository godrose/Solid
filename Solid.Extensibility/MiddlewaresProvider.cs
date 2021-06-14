using System.Collections.Generic;
using System.Linq;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    public class MiddlewaresProvider<TExtensible> : IMiddlewaresProvider<TExtensible>
        where TExtensible : class
    {
        public MiddlewaresProvider(IEnumerable<IMiddleware<TExtensible>> middlewares)
        {
            Middlewares = middlewares.ToArray();
        }

        public IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }
}