using System;
using System.Linq.Expressions;

namespace Solid.Data.Fake.Core
{
    public interface IMock<TFaked> where TFaked : class
    {
        void VerifyCall(Expression<Action<TFaked>> expression);
        void VerifyNoCall(Expression<Action<TFaked>> expression);
    }
}
