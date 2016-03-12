using System.Composition;

namespace Solid.Practices.Composition.Platform.UWP.Tests
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