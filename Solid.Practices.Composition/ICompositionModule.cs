using Solid.Practices.IoC;

namespace Solid.Practices.Composition
{
    public interface ICompositionModule
    {
        
    }

    public interface ICompositionModule<TIocContainer> : ICompositionModule where TIocContainer : IIocContainer
    {
        void RegisterModule(TIocContainer iocContainer);
    }
}