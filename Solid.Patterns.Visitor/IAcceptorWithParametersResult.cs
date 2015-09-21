namespace Solid.Patterns.Visitor
{    
    public interface IAcceptorWithParametersResult<in TVisitor, in T, out TResult>
    {
        TResult Accept(TVisitor visitor, T arg);
    }

    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, out TResult>
    {
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2);
    }

    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, out TResult>
    {
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, in T4, out TResult>
    {
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IAcceptorWithParametersResult<in TVisitor, in T1, in T2, in T3, in T4, in T5, out TResult>
    {
        TResult Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}
