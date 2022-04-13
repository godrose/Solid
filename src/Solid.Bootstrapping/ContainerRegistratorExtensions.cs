using System.Collections.Generic;
using System.Linq;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// The ioc container registrator extension methods.
    /// </summary>
    public static class ContainerRegistratorExtensions
    {
        /// <summary>
        /// Registers the composition modules into the ioc container.
        /// </summary>
        /// <typeparam name="TIocContainer">The type of the ioc container.</typeparam>
        /// <param name="iocContainer">The dependency registrator.</param>
        /// <param name="compositionModules">The composition modules.</param>
        public static void RegisterContainerAdapterCompositionModules<TIocContainer>(
            this TIocContainer iocContainer,
            IEnumerable<ICompositionModule> compositionModules)
            where TIocContainer : class, IDependencyRegistrator
        {
            var modules = compositionModules as ICompositionModule[] ?? compositionModules.ToArray();
            var middlewares = new List<IMiddleware<TIocContainer>>(new IMiddleware<TIocContainer>[]
            {
                new ContainerRegistrationMiddleware<TIocContainer, IDependencyRegistrator>(modules),
                new ContainerPlainRegistrationMiddleware<TIocContainer>(modules),
                new ContainerHierarchicalRegistrationMiddleware<TIocContainer>(modules)
            });
            MiddlewareApplier.ApplyMiddlewares(iocContainer, middlewares);
        }
    }
}