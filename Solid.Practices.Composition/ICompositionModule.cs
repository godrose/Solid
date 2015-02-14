using Solid.Practices.IoC;

namespace Solid.Practices.Composition
{
    public interface ICompositionModule
    {
        void RegisterModule(IIocContainer container);
    }
}