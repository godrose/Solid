namespace Solid.Patterns.Visitor
{    
    /// <summary>
    /// Represents acceptor with return value and one parameter
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T">Type of parameter</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>
    public interface IAcceptorWithParametersResult<in TVisitor, in T, out TResult>
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="arg">The argument.</param>
        /// <returns></returns>
        TResult Accept(TVisitor visitor, T arg);
    }

    /// <summary>
    /// Represents acceptor with return value and two parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>
    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, out TResult>
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>       
        /// <returns></returns>
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Represents acceptor with return value and two parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>
    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, out TResult>
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>        
        /// <returns></returns>
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3);
    }

    /// <summary>
    /// Represents acceptor with return value and two parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    /// <typeparam name="T4">Type of fourth parameter</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>    
    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, in T4, out TResult>
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>        
        /// <returns></returns>
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    /// <summary>
    /// Represents acceptor with return value and two parameters
    /// </summary>
    /// <typeparam name="TVisitor">Type of visitor</typeparam>
    /// <typeparam name="T1">Type of first parameter</typeparam>
    /// <typeparam name="T2">Type of second parameter</typeparam>
    /// <typeparam name="T3">Type of third parameter</typeparam>
    /// <typeparam name="T4">Type of fourth parameter</typeparam>
    /// <typeparam name="T5">Type of fifth parameter</typeparam>
    /// <typeparam name="TResult">Type of return value</typeparam>
    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, in T4, in T5, out TResult>
    {
        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        /// <param name="arg5">The fifth argument.</param>
        /// <returns></returns>
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}
