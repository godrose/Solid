using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// The extensibility by type aspect.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtensibilityByTypeAspect<T> :
        IAspect,
        IExtensibleByType<T> where T : class
    {
        private readonly T _extensible;
        private readonly MiddlewareTypesWrapper<T> _middlewareTypesWrapper;

        /// <summary>
        /// Creates an instance of <see cref="ExtensibilityByTypeAspect{T}"/>
        /// </summary>
        /// <param name="extensible"></param>
        /// <param name="iocContainer"></param>
        public ExtensibilityByTypeAspect(T extensible, IIocContainer iocContainer)
        {
            _extensible = extensible;
            _middlewareTypesWrapper = new MiddlewareTypesWrapper<T>(_extensible, iocContainer);
        }

        /// <inheritdoc />       
        public T Use<TMiddleware>() where TMiddleware : class, IMiddleware<T>
        {
            _middlewareTypesWrapper.Use<TMiddleware>();
            return _extensible;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            var middlewaresCollection = _middlewareTypesWrapper.Build();
            MiddlewareApplier.ApplyMiddlewares(_extensible, middlewaresCollection.Middlewares);
        }

        /// <inheritdoc />
        public string Id => $"ExtensibilityByType.{typeof(T).FullName}";

        /// <inheritdoc />
        public string[] Dependencies => new[] { "Modularity", "Discovery", "Platform" };
    }
}