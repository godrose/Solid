using Solid.Practices.IoC;

namespace Solid.Practices.Middleware
{
    /// <summary>
    /// Represents an interface for middleware that is applied to an IoC container.
    /// </summary>
    public interface IMiddleware
    {
        /// <summary>
        /// Applies the middleware on the specified IoC container.
        /// </summary>
        /// <typeparam name="TIocContainer">The type of the IoC container.</typeparam>
        /// <param name="iocContainer">The IoC container.</param>
        /// <returns></returns>
        TIocContainer Apply<TIocContainer>(TIocContainer iocContainer) where TIocContainer : class, IIocContainer;
    }
}
