using Solid.Core;
using System;
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
            IEnumerable<IMiddleware<T>> middlewares) where T : class => AggregateMiddlewares(@object, SortMiddlewares(middlewares));

        private static IEnumerable<IMiddleware<T>> SortMiddlewares<T>
            (IEnumerable<IMiddleware<T>> middlewares) where T : class
        {
            const string sameKeyPrefix = "An item with the same key has already been added. Key: ";
            try
            {
                var result = new List<IMiddleware<T>>();
                var sortedItems = TopologicalSort.Sort<object, string>(middlewares, ExtractDependencies, ExtractId, ignoreCycles: false).OfType<IMiddleware<T>>();
                result.Clear();
                result.AddRange(sortedItems);
                return result;
            }
            catch (ArgumentException e)
            {
                if (e.Message.StartsWith(sameKeyPrefix))
                {
                    throw new Exception($"Id must be unique - {e.Message.Substring(sameKeyPrefix.Length)}");
                }

                throw;
            }
            catch (KeyNotFoundException e)
            {
                var parts = e.Message.Split('\'');
                //TODO: Use RegEx
                if (parts.Length == 3)
                {
                    throw new Exception($"Missing dependency {parts[1]}");
                }
                throw;
            }            
        }

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