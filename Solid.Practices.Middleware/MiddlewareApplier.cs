using Solid.Core;
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
                middlewares.SortTopologically<object, string>(ExtractDependencies, ExtractId)
                    .OfType<IMiddleware<T>>());        

        private static string ExtractId(object arg)
        {            
            if (arg is IIdentifiable identifiable)
            {
                return identifiable.Id;
            }
            var idAttributes = arg.GetType().GetCustomAttributes(typeof(IdAttribute), inherit: true).OfType<IdAttribute>();
            var idAttribute = idAttributes.FirstOrDefault();
            return idAttribute != null ? idAttribute.Id : arg.GetType().Name;
        }

        private static IEnumerable<string> ExtractDependencies(object arg)
        {            
            if (arg is IHaveDependencies haveDependencies)
            {
                return haveDependencies.Dependencies;
            }
            var depAttributes = arg.GetType().GetCustomAttributes(typeof(DependenciesAttribute), inherit:true).OfType<DependenciesAttribute>();
            var depAttribute = depAttributes.FirstOrDefault();
            return depAttribute != null ? depAttribute.Dependencies : new string[] { };
        }

        private static void AggregateMiddlewares<T>
        (T @object,
            IEnumerable<IMiddleware<T>> middlewares) where T : class =>
            middlewares.Aggregate(@object, (current, middleware) => middleware.Apply(current));
    }
}