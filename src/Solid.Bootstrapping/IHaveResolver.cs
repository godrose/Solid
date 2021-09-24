using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application whose architecture includes dependency resolver (service locator).
    /// </summary>
    public interface IHaveResolver
    {
        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>
        /// The resolver.
        /// </value>
        IDependencyResolver Resolver { get; }
    }
}