using System;
using System.Linq.Expressions;

namespace Solid.Fake.Moq
{
    public static class It
    {
        public static TValue IsAny<TValue>()
        {
            return global::Moq.It.IsAny<TValue>();
        }

        public static TValue Is<TValue>(Expression<Func<TValue, bool>> testExpression)
        {
            return global::Moq.It.Is(testExpression);
        }
    }
}
