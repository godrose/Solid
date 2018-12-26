using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// The extensibility aspect.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtensibilityAspect<T> :
        IAspect,
        IExtensible<T> where T : class
    {
        private readonly T _extensible;

        /// <summary>
        /// Creates an instance of <see cref="ExtensibilityAspect{T}"/>
        /// </summary>
        /// <param name="extensible"></param>
        public ExtensibilityAspect(T extensible)
        {
            _extensible = extensible;
            _middlewaresWrapper = new MiddlewaresWrapper<T>(_extensible);
        }

        private readonly MiddlewaresWrapper<T> _middlewaresWrapper;

        /// <inheritdoc />       
        public T Use(
            IMiddleware<T> middleware)
        {
            _middlewaresWrapper.Use(middleware);
            return _extensible;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            MiddlewareApplier.ApplyMiddlewares(_extensible, _middlewaresWrapper.Middlewares);
        }

        /// <inheritdoc />
        public string Id => $"Extensibility.{typeof(T).Name}";

        /// <inheritdoc />
        public string[] Dependencies => new[] { "Modularity", "Discovery", "Platform" };
    }
}