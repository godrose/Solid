namespace Solid.Patterns.Visitor
{    
    /// <summary>
    /// Represents acceptor without return value and one parameter
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T">Type of parameter</typeparam>
    public interface IAcceptorWithParameters<in TVisitor, in T>
    {
        void Accept(TVisitor visitor, T arg);
    }

    /// <summary>
    /// Represents acceptor without return value and two parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Represents acceptor without return value and three parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3);
    }

    /// <summary>
    /// Represents acceptor without return value and four parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    /// <typeparam name="T4">Type of fourth parameter</typeparam>
    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3, in T4>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    /// <summary>
    /// Represents acceptor without return value and four parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    /// <typeparam name="T4">Type of fourth parameter</typeparam>
    /// <typeparam name="T5">Type of fifth parameter</typeparam>
    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3, in T4, in T5>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}