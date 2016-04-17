using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application whose architecture includes ioc container adapter.
    /// </summary>
    /// <typeparam name="TIocContainerAdapter">The type of the ioc container adapter.</typeparam>
    public interface IHaveContainerAdapter<TIocContainerAdapter>
        where TIocContainerAdapter : IIocContainer
    {
        /// <summary>
        /// Gets the ioc container adapter.
        /// </summary>
        /// <value>
        /// The ioc container adapter.
        /// </value>
        TIocContainerAdapter ContainerAdapter { get; }
    }
}