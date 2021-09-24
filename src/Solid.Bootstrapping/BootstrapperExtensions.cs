using System.Collections.Generic;
using System.Linq;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// The bootstrapper extension methods.
    /// </summary>
    public static class BootstrapperExtensions
    {        
        /// <summary>
        /// Extends the bootstrapper's functionality by using the specified collection
        /// of dependency registrator middlewares.
        /// </summary>
        /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <param name="middlewares">The middlewares.</param>
        /// <returns></returns>
        public static TBootstrapper UseMany<TBootstrapper>(
            this TBootstrapper bootstrapper,
            IEnumerable<IMiddleware<IDependencyRegistrator>> middlewares)
            where TBootstrapper : class, IHaveRegistrator, IExtensible<TBootstrapper>
        {
            var bootstrapperMiddlewares =
                middlewares.Select(
                    t =>
                        new UseDependencyRegistratorMiddleware<TBootstrapper>(t));
            foreach (var bootstrapperMiddleware in bootstrapperMiddlewares)
            {
                bootstrapper.Use(bootstrapperMiddleware);
            }
            return bootstrapper;
        }

        /// <summary>
        /// Allows using composition modules for custom dependency registrator types.
        /// </summary>
        /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
        /// <typeparam name="TDependencyRegistrator">The type of the dependency registrator.</typeparam>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns></returns>
        public static TBootstrapper UseCompositionModules<TBootstrapper, TDependencyRegistrator>(
            this TBootstrapper bootstrapper)
            where TBootstrapper : class, IExtensible<TBootstrapper>,
            IHaveRegistrator<TDependencyRegistrator>, ICompositionModulesProvider
            where TDependencyRegistrator : class
        {
            return bootstrapper.Use(new RegisterCustomCompositionModulesMiddleware<TBootstrapper, TDependencyRegistrator>());
        }
    }
}
