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
        /// Registers collection of modules into the container.
        /// </summary>
        /// <typeparam name="TModule">The type of the module.</typeparam>
        /// <param name="container">The type of the container</param>
        /// <param name="modules">The collection of modules.</param>
        public static void RegisterModules<TModule>(IIocContainer container,
            IEnumerable<ICompositionModule> modules)
        {
            var typedModules = modules.OfType<TModule>().ToArray();
            container.RegisterInstance<IEnumerable<TModule>>(typedModules);
        }

        //TODO: non-generic version -> depends on IocContainer.RegisterCollection with appropriate signature.
    }
}