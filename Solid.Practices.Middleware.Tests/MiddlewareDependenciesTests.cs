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
        public string[] Dependencies => new string[] { "B", "C" };

        public string Id => "A";

        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("A");
        }
    }

    class MiddlewareB : IMiddleware<StringBuilder>, IHaveDependencies, IIdentifiable
    {
        public string[] Dependencies => new string[] { "C" };

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
}


