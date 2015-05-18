using System;
using System.Linq.Expressions;
using Solid.Fake.Core;

namespace Solid.Fake.Builders
{
    public abstract class FakeBuilderBase<TService> : IMock<TService> where TService : class
    {
        protected readonly IFake<TService> FakeService;

        protected FakeBuilderBase(IFakeFactory fakeFactory)
        {
            FakeService = fakeFactory.CreateFake<TService>();
        }

        protected abstract void SetupFake();

        public TService GetService()
        {
            SetupFake();
            return FakeService.Object;
        }

        public void VerifyCall(Expression<Action<TService>> expression)
        {
            FakeService.VerifyCall(expression);
        }

        public void VerifyNoCall(Expression<Action<TService>> expression)
        {
            FakeService.VerifyNoCall(expression);
        }

        public void VerifySingleCall(Expression<Action<TService>> expression)
        {
            FakeService.VerifySingleCall(expression);
        }

        public TService Object
        {
            get { return FakeService.Object; }
        }
    }
}
