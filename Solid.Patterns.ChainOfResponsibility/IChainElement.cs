namespace Solid.Patterns.ChainOfResponsibility
{
    public interface IChainElement<TData>
    {
        void Execute(TData data);
        IChainElement<TData> SetNext(IChainElement<TData> successor);
    }

    public interface IChainElement<TData, TResult>
    {
        TResult Execute(TData data);
        IChainElement<TData, TResult> SetNext(IChainElement<TData, TResult> successor);
    }
}
