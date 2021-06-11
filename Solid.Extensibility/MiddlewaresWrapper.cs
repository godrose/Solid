using System.Collections.Generic;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Wraps collection of middlewares.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MiddlewaresWrapper<T> : IExtensible<T>, IMiddlewaresReadOnlyCollection<T> where T : class
    {
        private readonly T _object;
        private readonly List<IMiddleware<T>> _middlewares = new List<IMiddleware<T>>();

        /// <summary>
        /// Creates new instance of <see cref="MiddlewaresWrapper{T}"/>
        /// </summary>
        /// <param name="object"></param>
        public MiddlewaresWrapper(T @object)
        {
            _object = @object;
        }

        /// <inheritdoc />
        public T Use(IMiddleware<T> middleware)
        {
            _middlewares.Add(middleware);
            return _object;
        }

        /// <inheritdoc/>
        public IEnumerable<IMiddleware<T>> Middlewares => _middlewares;
    }
}