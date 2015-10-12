namespace Solid.Patterns.ChainOfResponsibility
{
    public interface IChainElement<TData> : ICanSetSuccessor<IChainElement<TData>>
    {
        void Handle(TData data);        
    }

    public interface IChainElement<TData, TResult> : ICanSetSuccessor<IChainElement<TData, TResult>>
    {
        TResult Handle(TData data);        
    }
}
