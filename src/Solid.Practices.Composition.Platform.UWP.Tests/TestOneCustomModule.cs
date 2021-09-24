using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Platform.UWP.Tests
{
    interface ICustomModule : ICompositionModule
    {

    }   
    
    class TestOneCustomModule : ICustomModule
    {
    }
    
    class TestTwoCustomModule : ICustomModule
    {
    }
}