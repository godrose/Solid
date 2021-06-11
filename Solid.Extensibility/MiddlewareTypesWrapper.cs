using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Patterns.Builder;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    public class MiddlewareTypesWrapper<TExtensible> :
        IExtensibleByType<TExtensible>,
        IBuilder<MiddlewaresReadOnlyCollection<TExtensible>>
        where TExtensible : class
    {
        private readonly TExtensible _object;
        private readonly IIocContainer _iocContainer;
        private readonly List<Type> _middlewareTypes = new List<Type>();

        public MiddlewareTypesWrapper(
            TExtensible @object,
            IIocContainer iocContainer)
        {
            _object = @object;
            _iocContainer = iocContainer;
        }

        /// <inheritdoc/>
        public MiddlewaresReadOnlyCollection<TExtensible> Build()
        {
            return new MiddlewaresReadOnlyCollection<TExtensible>(
                _middlewareTypes.Select(t => (IMiddleware<TExtensible>) _iocContainer.Resolve(t)));
        }

        /// <inheritdoc/>
        public TExtensible Use<TExtension>() where TExtension : class, IMiddleware<TExtensible>
        {
            _middlewareTypes.Add(typeof(TExtension));
            _iocContainer.RegisterSingleton<TExtension>();
            return _object;
        }
    }
}