using System.Text;
using Solid.Core;

namespace Solid.Practices.Middleware.Specs
{
    class IndependentImplicitMiddleware : IMiddleware<StringBuilder>
    {
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("IndImpl");
        }
    }

    class IndependentExplicitMiddleware : IMiddleware<StringBuilder>, IHaveDependencies
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

    [Dependencies(new string[] { })]
    [Id("C")]
    class MiddlewareAttrC : IMiddleware<StringBuilder>
    {
        public StringBuilder Apply(StringBuilder @object)
        {
            return @object.Append("C");
        }
    }
}
