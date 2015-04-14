using System;
using System.Linq.Expressions;

namespace Solid.Data.Fake.Core
{
    public interface IFake<TFaked> : IMock<TFaked> where TFaked: class
    {
        IFake<TFaked> SetupWithResult<TResult>(Expression<Func<TFaked, TResult>> expression, TResult result);
        IFake<TFaked> SetupWithException<TResult>(Expression<Func<TFaked, TResult>> expression, Exception exception);
        TFaked Object { get; }
    }

    public interface IFakeFactory
    {
        IFake<TFaked> CreateFake<TFaked>() where TFaked : class;
    }
}
