using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Patterns.Builder;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    public class MiddlewareTypesWrapper<TExtensible> :
        IBuilder<MiddlewaresReadOnlyCollection<TExtensible>>
        where TExtensible : class
    {
        private readonly IIocContainer _iocContainer;
        private readonly List<Type> _middlewareTypes = new List<Type>();

        public MiddlewareTypesWrapper(
            IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public MiddlewaresReadOnlyCollection<TExtensible> Build()
        {
            return new MiddlewaresReadOnlyCollection<TExtensible>(
                _middlewareTypes.Select(t => (IMiddleware<TExtensible>) _iocContainer.Resolve(t)));
        }

        public void Use<TExtension>() where TExtension : class, IMiddleware<TExtensible>
        {
            _middlewareTypes.Add(typeof(TExtension));
            _iocContainer.RegisterSingleton<TExtension>();
        }
    }
}