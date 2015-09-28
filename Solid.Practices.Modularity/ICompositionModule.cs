using Solid.Practices.IoC;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Represents a composistion module.
    /// It is a marker interface, meant to be used via MEF
    /// Import and Export attributes
    /// </summary>
    public interface ICompositionModule
    {
        
    }

    /// <summary>
    /// Represents a composition module, which may be registered into IoC container
    /// </summary>
    /// <typeparam name="TIocContainer">Type of IoC container</typeparam>
    public interface ICompositionModule<in TIocContainer> : ICompositionModule where TIocContainer : IIocContainer
    {
        /// <summary>
        /// Registers composition module into IoC container
        /// </summary>
        /// <param name="iocContainer">IoC container</param>
        void RegisterModule(TIocContainer iocContainer);
    }
}