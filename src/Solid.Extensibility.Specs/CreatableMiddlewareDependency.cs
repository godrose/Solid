using JetBrains.Annotations;

namespace Solid.Extensibility.Specs
{
    internal interface ICreatableMiddlewareDependency
    {
        bool IsCalled { get; set; }
    }

    [UsedImplicitly]
    internal sealed class CreatableMiddlewareDependency : ICreatableMiddlewareDependency
    {
        public bool IsCalled { get; set; }
    }
}
