using Solid.Common;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class LifecycleHook
    {
        [AfterScenario]
        public void Teardown()
        {
            PlatformProvider.Current = new DefaultPlatformProvider();
            AssemblyLoader.LoadAssembliesFromPaths = AssemblyLoader.DefaultLoader;
        }
    }
}
