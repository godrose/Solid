using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Core;
using Solid.Patterns.Builder;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Wraps collection of middleware types.
    /// </summary>
    /// <typeparam name="TExtensible">The type of the extensible object.</typeparam>
    public class MiddlewareTypesWrapper<TExtensible> :
        IExtensibleByType<TExtensible>,
        IBuilder<MiddlewaresProvider<TExtensible>>,
        IHaveErrors
        where TExtensible : class
    {
        private readonly List<Exception> _errors = new List<Exception>();
        private readonly IIocContainer _iocContainer;
        private readonly List<Type> _middlewareTypes = new List<Type>();
        private readonly TExtensible _object;

        /// <summary>
        /// Creates new instance of <see cref="MiddlewareTypesWrapper{T}"/>
        /// </summary>
        /// <param name="object">The extensible object.</param>
        /// <param name="iocContainer">The IoC container.</param>
        public MiddlewareTypesWrapper(
            TExtensible @object,
            IIocContainer iocContainer)
        {
            _object = @object;
            _iocContainer = iocContainer;
        }

        /// <inheritdoc />
        public MiddlewaresProvider<TExtensible> Build()
        {
            return new MiddlewaresProvider<TExtensible>(
                _middlewareTypes
                    .Select(t =>
                    {
                        try
                        {
                            return (IMiddleware<TExtensible>) _iocContainer.Resolve(t);
                        }
                        catch (Exception e)
                        {
                            _errors.Add(e);
                            return null;
                        }
                    })
                    .Where(t => t != null));
        }

        /// <inheritdoc />
        public TExtensible Use<TExtension>() where TExtension : class, IMiddleware<TExtensible>
        {
            _middlewareTypes.Add(typeof(TExtension));
            _iocContainer.RegisterSingleton<TExtension>();
            return _object;
        }

        /// <inheritdoc />
        public IEnumerable<Exception> Errors => _errors;
    }
}