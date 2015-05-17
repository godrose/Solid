using System;
using System.Linq.Expressions;
using FakeItEasy;

namespace Solid.Fake.FakeItEasy
{
    public static class It
    {
        public static TValue IsAny<TValue>()
        {
            return A<TValue>.Ignored;
        }

        public static TValue Is<TValue>(Expression<Func<TValue, bool>> testExpression)
        {
            return A<TValue>.That.Matches(testExpression);
        }
    }
}
