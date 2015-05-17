using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    public interface ICompositionModule
    {
        
    }

    public interface ICompositionModule<in TIocContainer> : ICompositionModule where TIocContainer : IIocContainer
    {
        void RegisterModule(TIocContainer iocContainer);
    }
}