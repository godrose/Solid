using System;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Uses <see cref="Activator"/> for creating composition modules.
    /// </summary>
    /// <seealso cref="IModuleCreationStrategy" />
    public class ActivatorCreationStrategy : IModuleCreationStrategy
    {
        /// <summary>
        /// Creates module from its type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateModule(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }

    /// <summary>
    /// Uses <see cref="IIocContainer"/> for creating composition modules.
    /// </summary>
    /// <seealso cref="IModuleCreationStrategy" />
    public class ContainerResolutionStrategy : IModuleCreationStrategy
    {
        private readonly IIocContainer _iocContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerResolutionStrategy"/> class.
        /// </summary>
        /// <param name="iocContainer">The ioc container.</param>
        public ContainerResolutionStrategy(IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        /// <summary>
        /// Creates module from its type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateModule(Type type)
        {
            _iocContainer.RegisterSingleton(type, type);
            return _iocContainer.Resolve(type);
        }
    }
}
