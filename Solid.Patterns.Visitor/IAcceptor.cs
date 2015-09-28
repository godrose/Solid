namespace Solid.Patterns.Visitor
{
    /// <summary>
    /// Represents acceptor without return value
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    public interface IAcceptor<in TVisitor>
    {
        void Accept(TVisitor visitor);
    }

    /// <summary>
    /// Represents acceptor with return value
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>
    public interface IAcceptor<in TVisitor, out TResult>
    {
        TResult Accept(TVisitor visitor);
    }    
}
