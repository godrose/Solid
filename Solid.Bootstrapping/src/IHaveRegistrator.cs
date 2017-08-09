using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application whose architecture includes dependency registrator.
    /// </summary>
    public interface IHaveRegistrator
    {
        /// <summary>
        /// Gets the registrator.
        /// </summary>
        /// <value>
        /// The registrator.
        /// </value>
        IDependencyRegistrator Registrator { get; }
    }
}