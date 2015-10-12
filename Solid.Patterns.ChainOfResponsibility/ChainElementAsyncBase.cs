using System.Threading.Tasks;

namespace Solid.Patterns.ChainOfResponsibility
{
    public abstract class ChainElementAsyncBase<TData> :
        IChainElementAsync<TData> where TData : class
    {
        private IChainElementAsync<TData> _successor;

        protected abstract Task<bool> HandleDataAsync(TData data);
        protected abstract bool IsMine(TData data);

        public async Task HandleAsync(TData request)
        {
            if (IsMine(request))
            {
                var isHandled = await HandleDataAsync(request);
                if (isHandled == false)
                {
                    await _successor.HandleAsync(request);
                }
            }
        }

        public IChainElementAsync<TData> SetSuccessor(IChainElementAsync<TData> successor)
        {
            _successor = successor;
            return _successor;
        }
    }

    public abstract class ChainElementAsyncBase<TData, TResult> : IChainElementAsync<TData, TResult>
    {
        private IChainElementAsync<TData, TResult> _successor;

        protected abstract bool IsMine(TData data);

        protected abstract Task<TResult> HandleDataAsync(TData data);

        public async Task<TResult> HandleAsync(TData data)
        {
            //deliberately throw an exception if no successor is set
            return IsMine(data) ? await HandleDataAsync(data) : await _successor.HandleAsync(data);
        }

        public IChainElementAsync<TData, TResult> SetSuccessor(IChainElementAsync<TData, TResult> successor)
        {
            _successor = successor;
            return _successor;
        }
    }
}