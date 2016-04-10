using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.WIN81.Tests
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
