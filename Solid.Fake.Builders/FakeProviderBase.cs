using System;

namespace Solid.Fake.Builders
{
    public abstract class FakeProviderBase<TBuilder, TService>
        where TService : class
        where TBuilder : FakeBuilderBase<TService>
    {
        protected TService GetService(Func<TBuilder> createBuilder, Func<TBuilder, TBuilder> setupMiddleware)
        {
            var builder = createBuilder();
            builder = setupMiddleware(builder);
            return builder.GetService();
        }
    }
}
