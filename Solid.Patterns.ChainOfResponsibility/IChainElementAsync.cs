using System.Threading.Tasks;

namespace Solid.Patterns.ChainOfResponsibility
{
    public interface IChainElementAsync<TData>
    {
        Task ExecuteAsync(TData data);
        IChainElementAsync<TData> SetNext(IChainElementAsync<TData> successor);
    }

    public interface IChainElementAsync<TData, TResult>
    {
        Task<TResult> ExecuteAsync(TData data);
        IChainElementAsync<TData, TResult> SetNext(IChainElementAsync<TData, TResult> successor);
    }
}