using System.Threading.Tasks;

namespace Solid.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// Represents chain element that allows asynchronous data handling.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <seealso cref="ChainOfResponsibility.IChainElementAsync{TData}" />
    public abstract class ChainElementAsyncBase<TData> :
        IChainElementAsync<TData> where TData : class
    {
        private IChainElementAsync<TData> _successor;        

        /// <summary>
        /// Determines whether the specified data can be handled by the chain element.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract bool IsMine(TData data);

        /// <summary>
        /// Handles the data asynchronously returning the value 
        /// which indicates whether the handling has been successful.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract Task<bool> HandleDataAsync(TData data);

        /// <summary>
        /// Handles the data asynchronously via the chain of responsibility.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task HandleAsync(TData data)
        {
            if (IsMine(data))
            {
                var isHandled = await HandleDataAsync(data);
                if (isHandled == false)
                {
                    await _successor.HandleAsync(data);
                }
            }
        }

        /// <summary>
        /// Sets the successor.
        /// </summary>
        /// <param name="successor">The successor.</param>
        /// <returns></returns>
        public IChainElementAsync<TData> SetSuccessor(IChainElementAsync<TData> successor)
        {
            _successor = successor;
            return _successor;
        }
    }

    /// <summary>
    /// Represents chain element that allows asynchronous data handling with return value.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <seealso cref="ChainOfResponsibility.IChainElementAsync{TData}" />
    public abstract class ChainElementAsyncBase<TData, TResult> : IChainElementAsync<TData, TResult>
    {
        private IChainElementAsync<TData, TResult> _successor;

        /// <summary>
        /// Determines whether the specified data can be handled by the chain element.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract bool IsMine(TData data);

        /// <summary>
        /// Handles the data asynchronously returning the value 
        /// which indicates whether the handling has been successful.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract Task<TResult> HandleDataAsync(TData data);

        /// <summary>
        /// Handles the data asynchronously via the chain of responsibility.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public async Task<TResult> HandleAsync(TData data)
        {
            //deliberately throw an exception if no successor is set
            return IsMine(data) ? await HandleDataAsync(data) : await _successor.HandleAsync(data);
        }

        /// <summary>
        /// Sets the successor.
        /// </summary>
        /// <param name="successor">The successor.</param>
        /// <returns></returns>
        public IChainElementAsync<TData, TResult> SetSuccessor(IChainElementAsync<TData, TResult> successor)
        {
            _successor = successor;
            return _successor;
        }
    }
}