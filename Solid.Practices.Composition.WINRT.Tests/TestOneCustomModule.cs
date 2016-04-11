using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.WINRT.Tests
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
