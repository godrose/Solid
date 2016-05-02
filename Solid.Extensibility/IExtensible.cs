using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an object whose functionality can be extended.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IExtensible<T> where T : class
    {
        /// <summary>
        /// Extends the functionality by using the specified middleware.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <returns></returns>
        T Use(IMiddleware<T> middleware);
    }
}
