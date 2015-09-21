namespace Solid.Patterns.Visitor
{    
    public interface IAcceptorWithParameters<in TVisitor, in T>
    {
        void Accept(TVisitor visitor, T arg);
    }

    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2);
    }

    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3, in T4>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IAcceptorWithParameters<in TVisitor, in T1, in T2, in T3, in T4, in T5>
    {
        void Accept(TVisitor visitor, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}