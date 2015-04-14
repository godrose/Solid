using System;
using System.Linq.Expressions;
using Moq;
using Solid.Fake.Core;

namespace Solid.Fake.Moq
{
    public class Fake<TFaked> : IFake<TFaked> where TFaked: class
    {
        private readonly Mock<TFaked> _mock;

        internal Fake(Mock<TFaked> mock)
        {
            _mock = mock;
        }
        

        public IFake<TFaked> SetupWithResult<TResult>(Expression<Func<TFaked, TResult>> expression, TResult result)
        {
            _mock.Setup(expression).Returns(result);
            return this;
        }

        public IFake<TFaked> SetupWithException<TResult>(Expression<Func<TFaked, TResult>> expression, Exception exception)
        {
            _mock.Setup(expression).Throws(exception);
            return this;
        }

        public TFaked Object
        {
            get { return _mock.Object; }
        }

        public void VerifyCall(Expression<Action<TFaked>> expression)
        {
            _mock.Verify(expression);
        }

        public void VerifyNoCall(Expression<Action<TFaked>> expression)
        {
            _mock.Verify(expression, Times.Never);
        }
    }
}
