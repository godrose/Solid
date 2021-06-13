namespace Solid.Extensibility.Specs
{
    interface ICreatableMiddlewareDependency
    {
        bool IsCalled { get; set; }
    }

    class CreatableMiddlewareDependency : ICreatableMiddlewareDependency
    {
        public bool IsCalled { get; set; }
    }
}
