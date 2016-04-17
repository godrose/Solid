namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents the root object of an application.
    /// </summary>
    /// <typeparam name="TRootObject">The type of the root object.</typeparam>
    public interface IHaveRootObject<TRootObject>
    {
        /// <summary>
        /// Gets the root object.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        TRootObject RootObject { get; }
    }
}