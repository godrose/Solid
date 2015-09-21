namespace Solid.Patterns.Visitor
{
    public interface IAcceptor<in TVisitor>
    {
        void Accept(TVisitor visitor);
    }

    public interface IAcceptor<in TVisitor, out TResult>
    {
        TResult Accept(TVisitor visitor);
    }    
}
