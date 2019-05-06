using FluentAssertions;
using Solid.Core;
using System.Text;
using Xunit;

namespace Solid.Practices.Middleware.Tests
{
    public class MiddlewareDependenciesTests
    {
        [Fact]
        public void Apply_MiddlewaresHaveDependencies_MiddlewaresAreInvokedInTheCorrectOrder()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareB(), new MiddlewareA(), new MiddlewareC() };

            var init = new StringBuilder();
            MiddlewareApplier.ApplyMiddlewares(init, middlewares);

            var result = init.ToString();
            result.Should().Be("CBA");
        }

        [Fact]
        public void Apply_SomeMiddlewaresHaveDependenciesSomeDont_MiddlewaresAreInvokedInTheCorrectOrder()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareB(), new MiddlewareA(), new IndependentExplicitMiddleware(), new MiddlewareC() };

            var init = new StringBuilder();
            MiddlewareApplier.ApplyMiddlewares(init, middlewares);

            var result = init.ToString();
            result.Should().Be("CBAIndExpl");
        }

        [Fact]
        public void Apply_SomeMiddlewaresHaveDependenciesSomeDontImplicit_MiddlewaresAreInvokedInTheCorrectOrder()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareB(), new MiddlewareA(), new IndependentImplicitMiddleware(), new MiddlewareC() };

            var init = new StringBuilder();
            MiddlewareApplier.ApplyMiddlewares(init, middlewares);

            var result = init.ToString();
            result.Should().Be("CBAIndImpl");
        }

        [Fact]
        public void Apply_MiddlewaresHaveDependenciesByAttributes_MiddlewaresAreInvokedInTheCorrectOrder()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareAttrB(), new MiddlewareAttrA(), new MiddlewareAttrC() };

            var init = new StringBuilder();
            MiddlewareApplier.ApplyMiddlewares(init, middlewares);

            var result = init.ToString();
            result.Should().Be("CBA");
        }
    }

    class IndependentImplicitMiddleware : IMiddleware<StringBuilder>
    {
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("IndImpl");
        }
    }

    class IndependentExplicitMiddleware : IMiddleware<StringBuilder> , IHaveDependencies
    {
        public string[] Dependencies => new string[] { };

        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("IndExpl");
        }
    }

    class MiddlewareA : IMiddleware<StringBuilder>, IHaveDependencies, IIdentifiable
    {
        public string[] Dependencies => new[] { "B", "C" };

        public string Id => "A";

        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("A");
        }
    }

    class MiddlewareB : IMiddleware<StringBuilder>, IHaveDependencies, IIdentifiable
    {
        public string[] Dependencies => new[] { "C" };

        public string Id => "B";

        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("B");
        }
    }

    class MiddlewareC : IMiddleware<StringBuilder>, IHaveDependencies, IIdentifiable
    {
        public string[] Dependencies => new string[] { };

        public string Id => "C";

        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("C");
        }
    }

    [Dependencies(new[] { "B", "C" })]
    [Id("A")]
    class MiddlewareAttrA : IMiddleware<StringBuilder>
    {        
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("A");
        }
    }

    [Dependencies(new[] { "C" })]
    [Id("B")]
    class MiddlewareAttrB : IMiddleware<StringBuilder>
    {
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("B");
        }
    }

    [Dependencies(new string[] {  })]
    [Id("C")]
    class MiddlewareAttrC : IMiddleware<StringBuilder>
    {
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("C");
        }
    }
}


