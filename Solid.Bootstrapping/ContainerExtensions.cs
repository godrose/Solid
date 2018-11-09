using System.Collections.Generic;
using System.Linq;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// The ioc container extension methods.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Registers the composition modules into the ioc container.
        /// </summary>
        /// <typeparam name="TIocContainer">The type of the ioc container.</typeparam>
        /// <param name="iocContainer">The ioc container.</param>
        /// <param name="compositionModules">The composition modules.</param>
        public static void RegisterContainerCompositionModules<TIocContainer>(this TIocContainer iocContainer,
            IEnumerable<ICompositionModule> compositionModules)
            where TIocContainer : class
        {
            var modules = compositionModules as ICompositionModule[] ?? compositionModules.ToArray();
            var middlewares = new List<IMiddleware<TIocContainer>>(new IMiddleware<TIocContainer>[]
            {
                new ContainerRegistrationMiddleware<TIocContainer, TIocContainer>(modules),
                new ContainerPlainRegistrationMiddleware<TIocContainer>(modules),
                new ContainerHierarchicalRegistrationMiddleware<TIocContainer>(modules)
            });

            MiddlewareApplier.ApplyMiddlewares(iocContainer, middlewares);
        }
    }
}