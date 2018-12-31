using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application whose architecture includes <see cref="IDependencyRegistrator"/> 
    /// as the dependency registrator abstraction.
    /// </summary>
    public interface IHaveRegistrator : IHaveRegistrator<IDependencyRegistrator>
    {       
    }

    /// <summary>
    /// Represents an application whose architecture includes dependency registrator.
    /// </summary>
    /// <typeparam name="TDependencyRegistrator"></typeparam>
    public interface IHaveRegistrator<TDependencyRegistrator>
    {
        /// <summary>
        /// Gets the registrator.
        /// </summary>
        /// <value>
        /// The registrator.
        /// </value>
        TDependencyRegistrator Registrator { get; }
    }
}