using Solid.Practices.IoC;

namespace Solid.Practices.Middleware
{
    /// <summary>
    /// Represents an interface for middleware that is applied to an object.
    /// <typeparam name="T">The type of the object.</typeparam>
    /// 
    /// </summary>
    public interface IMiddleware<T> where T : class
    {
        /// <summary>
        /// Applies the middleware on the specified object.
        /// </summary>        
        /// <param name="object">The object.</param>
        /// <returns></returns>
        T Apply(T @object);
    }

    /// <summary>
    /// Represents an interface for middleware that is applied
    /// on an implementation of <see cref="IIocContainer"/>
    /// </summary>
    public interface IMiddleware : IMiddleware<IIocContainer>
    {
        
    }
}
