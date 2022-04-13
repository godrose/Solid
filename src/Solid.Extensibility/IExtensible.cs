using Solid.Practices.Middleware;

namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an object whose functionality can be extended via middleware instance.
    /// </summary>
    /// <typeparam name="TExtensible">The type of the extensible object.</typeparam>
    public interface IExtensible<TExtensible> where TExtensible : class
    {
        /// <summary>
        /// Extends the functionality by using the specified middleware.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <returns></returns>
        TExtensible Use(IMiddleware<TExtensible> middleware);
    }
}
