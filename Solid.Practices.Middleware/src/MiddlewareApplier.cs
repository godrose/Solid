using System.Collections.Generic;
using System.Linq;

namespace Solid.Practices.Middleware
{
    /// <summary>
    /// Applies middlewares onto the provided object.
    /// </summary>    
    public static class MiddlewareApplier
    {
        /// <summary>
        /// Applies the middlewares.
        /// </summary>        
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="object">The object.</param>
        /// <param name="middlewares">The middlewares.</param>
        public static void ApplyMiddlewares<T>(
            T @object,
            IEnumerable<IMiddleware<T>> middlewares) where T : class
        {
            ApplyMiddlewaresInternal(@object, middlewares);
        }

        static void ApplyMiddlewaresInternal<T>
            (T @object,
                IEnumerable<IMiddleware<T>> middlewares) where T : class
        {
            middlewares.Aggregate(@object, (current, middleware) => middleware.Apply(current));
        }
    }
}