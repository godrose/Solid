using System.Composition;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Platform.UWP.Tests
{
    [Export(typeof(ICompositionModule))]
    class TestCompositionModule : ICompositionModule
    {
    }
}