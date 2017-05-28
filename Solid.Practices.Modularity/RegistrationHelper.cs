using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Registration helper.
    /// </summary>
    public class RegistrationHelper
    {
        /// <summary>
        /// Registers collection of modules into the container.
        /// </summary>
        /// <typeparam name="TModule">The type of the module.</typeparam>
        /// <param name="containerRegistrator">The container registrator</param>
        /// <param name="modules">The collection of modules.</param>
        public static void RegisterModules<TModule>(
            IIocContainerRegistrator containerRegistrator,
            IEnumerable<ICompositionModule> modules) where TModule : class
        {
            var typedModules = modules.OfType<TModule>().ToArray();
            containerRegistrator.RegisterCollection(typedModules);
        }

        /// <summary>
        /// Registers the modules into the container.
        /// </summary>
        /// <param name="containerRegistrator">The container registrator.</param>
        /// <param name="contractType">The type of the contract.</param>
        /// <param name="modules">The modules.</param>
        public static void RegisterModules(
            IIocContainerRegistrator containerRegistrator,
            Type contractType,
            IEnumerable<ICompositionModule> modules)
        {
            RegisterCollection(containerRegistrator, contractType, modules.Select(t => t.GetType()));
        }

        /// <summary>
        /// Registers the collection of types that implement the specified contract
        /// into the container.
        /// </summary>
        /// <param name="containerRegistrator">The container registrator.</param>
        /// <param name="contractType">The type of the contract.</param>
        /// <param name="types">The types.</param>
        public static void RegisterCollection(
            IIocContainerRegistrator containerRegistrator,
            Type contractType,
            IEnumerable<Type> types)
        {
            var typeInfo = contractType.GetTypeInfo();
            var serviceTypes = types.Select(t => t.GetTypeInfo()).Where(t =>
                t.IsInterface == false && t.IsAbstract == false &&
                typeInfo.IsAssignableFrom(t)).Select(t => t.AsType());
            containerRegistrator.RegisterCollection(contractType, serviceTypes);
        }
    }
}