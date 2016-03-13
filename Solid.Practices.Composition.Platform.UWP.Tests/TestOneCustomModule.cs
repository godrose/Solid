using System.Composition;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Platform.UWP.Tests
{
    interface ICustomModule : ICompositionModule
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