namespace Solid.Patterns.ChainOfResponsibility
{
    public interface ICanSetSuccessor<TSuccessor>
    {
        TSuccessor SetSuccessor(TSuccessor successor);
    }
}