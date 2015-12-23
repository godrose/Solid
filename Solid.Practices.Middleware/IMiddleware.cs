using Solid.Practices.IoC;

namespace Solid.Practices.Middleware
{
    public interface IMiddleware
    {
        TIocContainer Apply<TIocContainer>(TIocContainer iocContainer) where TIocContainer : class, IIocContainer;
    }
}
