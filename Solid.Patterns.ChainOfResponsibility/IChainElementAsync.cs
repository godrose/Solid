using System.Threading.Tasks;

namespace Solid.Patterns.ChainOfResponsibility
{
    public interface IChainElementAsync<TData> : ICanSetSuccessor<IChainElementAsync<TData>>
    {
        Task HandleAsync(TData data);        
    }

    public interface IChainElementAsync<TData, TResult> : ICanSetSuccessor<IChainElementAsync<TData, TResult>>
    {
        Task<TResult> HandleAsync(TData data);        
    }
}