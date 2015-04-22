using System.Collections.Generic;
using Solid.Middleware;
using Solid.Practices.IoC;

namespace Solid.Tests.Core
{
    public class TestBootstrapper<TContainer> where TContainer : IIocContainer
    {
        public TestBootstrapper(TContainer iocContainer)
        {
            var middlewares = LoadMiddlewares();
            foreach (var middleware in middlewares)
            {
                middleware.Apply(iocContainer);
            }
        }

        private IEnumerable<IMiddleware> LoadMiddlewares()
        {
            return new IMiddleware[] {};
        }
    }
}
