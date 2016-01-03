using System.Threading.Tasks;

namespace Solid.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// Represents chain element which can be used in the chain-of-responsibility 
    /// design pattern asynchronous implementation.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>    
    public interface IChainElementAsync<TData> : ICanSetSuccessor<IChainElementAsync<TData>>
    {
        /// <summary>
        /// Handles the data asynchronously.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task HandleAsync(TData data);        
    }

    /// <summary>
    /// Represents chain element which can be used in the chain-of-responsibility 
    /// design pattern asynchronous implementation.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>    
    public interface IChainElementAsync<TData, TResult> : ICanSetSuccessor<IChainElementAsync<TData, TResult>>
    {
        /// <summary>
        /// Handles the data asynchronously.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task<TResult> HandleAsync(TData data);        
    }
}