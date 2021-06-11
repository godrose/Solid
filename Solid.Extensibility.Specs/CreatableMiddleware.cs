using Solid.Practices.Middleware;

namespace Solid.Extensibility.Specs
{
    internal class CreatableMiddleware : IMiddleware<ExtensibleByTypeObject>
    {
        public ExtensibleByTypeObject Apply(ExtensibleByTypeObject @object)
        {
            return @object;
        }
    }
}