﻿using Solid.Core;
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
            IEnumerable<IMiddleware<T>> middlewares) where T : class =>
            AggregateMiddlewares(@object,
                middlewares.SortTopologically(r => r.ExtractDependencies(), r => r.ExtractId()));        
        
        private static void AggregateMiddlewares<T>
        (T @object,
            IEnumerable<IMiddleware<T>> middlewares) where T : class =>
            middlewares.Aggregate(@object, (current, middleware) => middleware.Apply(current));
    }
}