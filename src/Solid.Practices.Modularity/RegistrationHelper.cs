using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Registration helper.
    /// </summary>
    public class RegistrationHelper
    {
        /// <summary>
        /// Registers collection of modules into the dependency registrator.
        /// </summary>
        /// <typeparam name="TModule">The type of the module.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="modules">The collection of modules.</param>
        public static void RegisterModules<TModule>(
            IDependencyRegistrator dependencyRegistrator,
            IEnumerable<ICompositionModule> modules) where TModule : class =>
            dependencyRegistrator.RegisterCollection(modules.OfType<TModule>().ToArray());

        /// <summary>
        /// Registers the modules into the dependency registrator.
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="contractType">The type of the contract.</param>
        /// <param name="modules">The collection of modules.</param>
        public static void RegisterModules(
            IDependencyRegistrator dependencyRegistrator,
            Type contractType,
            IEnumerable<ICompositionModule> modules) =>
            dependencyRegistrator.RegisterCollection(contractType, modules.Select(t => t.GetType()));
    }
}