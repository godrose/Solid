using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an object whose functionality can be extended via middleware type.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IExtensibleByType<T> where T : class
    {
        /// <summary>
        /// Extends the functionality by using the specified middleware type.
        /// </summary>
        /// <returns></returns>
        T Use<TExtension>() where TExtension : class, IMiddleware<T>;
    }
}