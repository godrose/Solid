namespace Solid.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// Represents chain element which can be used in the chain-of-responsibility 
    /// design pattern implementation.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>    
    public interface IChainElement<TData> : ICanSetSuccessor<IChainElement<TData>>
    {
        /// <summary>
        /// Handles the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        void Handle(TData data);        
    }

    /// <summary>
    /// Represents chain element which can be used in the chain-of-responsibility 
    /// design pattern implementation.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>    
    public interface IChainElement<TData, TResult> : ICanSetSuccessor<IChainElement<TData, TResult>>
    {
        /// <summary>
        /// Handles the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        TResult Handle(TData data);        
    }
}
