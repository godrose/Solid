using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application that has ioc container resolver (service locator).
    /// </summary>
    public interface IHaveResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>
        /// The resolver.
        /// </value>
        IIocContainerResolver Resolver { get; }
    }
}