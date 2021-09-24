namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application whose architecture includes IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC container.</typeparam>
    public interface IHaveContainer<TIocContainer>
    {
        /// <summary>
        /// Gets the ioc container.
        /// </summary>
        /// <value>
        /// The ioc container.
        /// </value>
        TIocContainer Container { get; }
    }
}