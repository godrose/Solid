using Solid.Practices.IoC;

namespace Solid.Middleware
{
    public interface IMiddleware
    {
        void Apply(IIocContainer iocContainer);
    }
}
