namespace Solid.Core
{
    /// <summary>
    /// Represents an identifiable object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIdentifiable<T>
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        T Id { get; }
    }

    /// <summary>
    /// Represents an identifiable object.
    /// </summary>
    public interface IIdentifiable : IIdentifiable<string>
    {
    
    }
}