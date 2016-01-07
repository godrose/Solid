using System.ComponentModel.Composition;

namespace Solid.Practices.Composition.Tests
{
    interface ICustomModule
    {
        
    }

    [Export(typeof(ICustomModule))]
    class TestOneCustomModule : ICustomModule
    {
    }

    [Export(typeof(ICustomModule))]
    class TestTwoCustomModule : ICustomModule
    {
    }
}
