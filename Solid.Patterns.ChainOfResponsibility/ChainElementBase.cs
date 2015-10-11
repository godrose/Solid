namespace Solid.Patterns.ChainOfResponsibility
{    
    public abstract class ChainElementBase<TData> :
        IChainElement<TData> where TData : class
    {
        private IChainElement<TData> _successor;

        protected abstract bool HandleData(TData data);
        protected abstract bool IsMine(TData data);

        public void Execute(TData request)
        {
            if (IsMine(request))
            {
                var isHandled = HandleData(request);
                if (isHandled == false)
                {
                    _successor.Execute(request);
                }
            }
        }

        public IChainElement<TData> SetNext(IChainElement<TData> successor)
        {
            _successor = successor;
            return _successor;
        }
    }

    public abstract class ChainElementBase<TData, TResult> : IChainElement<TData, TResult>
    {
        private IChainElement<TData, TResult> _successor;

        protected abstract bool IsMine(TData data);

        protected abstract TResult HandleData(TData data);

        public TResult Execute(TData data)
        {
            //deliberately throw an exception if no successor is set
            return IsMine(data) ? HandleData(data) : _successor.Execute(data);
        }

        public IChainElement<TData, TResult> SetNext(IChainElement<TData, TResult> successor)
        {
            _successor = successor;
            return _successor;
        }
    }
}