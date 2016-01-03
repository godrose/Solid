namespace Solid.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// Represents chain element that allows data handling.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <seealso cref="ChainOfResponsibility.IChainElement{TData}" />
    public abstract class ChainElementBase<TData> :
        IChainElement<TData> where TData : class
    {
        private IChainElement<TData> _successor;

        /// <summary>
        /// Determines whether the specified data can be handled by the chain element.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract bool IsMine(TData data);

        /// <summary>
        /// Handles the data returning the value 
        /// which indicates whether the handling has been successful.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract bool HandleData(TData data);

        /// <summary>
        /// Handles the data via the chain of responsibility.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public void Handle(TData data)
        {
            if (IsMine(data))
            {
                var isHandled = HandleData(data);
                if (isHandled == false)
                {
                    _successor.Handle(data);
                }
            }
        }

        /// <summary>
        /// Sets the successor.
        /// </summary>
        /// <param name="successor">The successor.</param>
        /// <returns></returns>
        public IChainElement<TData> SetSuccessor(IChainElement<TData> successor)
        {
            _successor = successor;
            return _successor;
        }
    }

    /// <summary>
    /// Represents chain element that allows data handling with return value.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <typeparam name="TResult">The type of the return value.</typeparam>
    /// <seealso cref="ChainOfResponsibility.IChainElement{TData}" />
    public abstract class ChainElementBase<TData, TResult> : IChainElement<TData, TResult>
    {
        private IChainElement<TData, TResult> _successor;

        /// <summary>
        /// Determines whether the specified data can be handled by the chain element.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract bool IsMine(TData data);

        /// <summary>
        /// Handles the data returning the value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected abstract TResult HandleData(TData data);

        /// <summary>
        /// Handles the data via the chain of responsibility returning the value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public TResult Handle(TData data)
        {
            //deliberately throw an exception if no successor is set
            return IsMine(data) ? HandleData(data) : _successor.Handle(data);
        }

        /// <summary>
        /// Sets the successor.
        /// </summary>
        /// <param name="successor">The successor.</param>
        /// <returns></returns>
        public IChainElement<TData, TResult> SetSuccessor(IChainElement<TData, TResult> successor)
        {
            _successor = successor;
            return _successor;
        }
    }
}