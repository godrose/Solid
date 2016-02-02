using System.Composition;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Tests
{
    [Export(typeof(ICompositionModule))]
    class TestCompositionModule : ICompositionModule
    {
    }
}
