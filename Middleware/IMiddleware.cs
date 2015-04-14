using Solid.Practices.IoC;

namespace Middleware
{
    public interface IMiddleware
    {
        void Apply(IIocContainer iocContainer);
    }
}
