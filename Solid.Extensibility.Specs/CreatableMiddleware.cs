using JetBrains.Annotations;
using Solid.Practices.Middleware;

namespace Solid.Extensibility.Specs
{
    [UsedImplicitly]
    internal class CreatableMiddleware : IMiddleware<ExtensibleByTypeObject>
    {
        private readonly ICreatableMiddlewareDependency _dependency;

        public CreatableMiddleware(ICreatableMiddlewareDependency dependency)
        {
            _dependency = dependency;
        }

        public ExtensibleByTypeObject Apply(ExtensibleByTypeObject @object)
        {
            _dependency.IsCalled = true;
            return @object;
        }
    }
}