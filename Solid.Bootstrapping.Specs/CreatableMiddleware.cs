using Solid.Practices.Middleware;

namespace Solid.Bootstrapping.Specs
{
    internal class CreatableMiddleware : IMiddleware<FakeBootstrapperWithExtensibilityByType>
    {
        private readonly ICreatableMiddlewareDependency _dependency;

        public CreatableMiddleware(ICreatableMiddlewareDependency dependency)
        {
            _dependency = dependency;
        }

        public FakeBootstrapperWithExtensibilityByType Apply(FakeBootstrapperWithExtensibilityByType @object)
        {
            _dependency.IsCalled = true;
            return @object;
        }
    }
}