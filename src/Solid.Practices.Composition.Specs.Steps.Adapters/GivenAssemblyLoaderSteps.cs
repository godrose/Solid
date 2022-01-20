using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Specs.Steps.Adapters
{
    [Binding]
    internal sealed class GivenAssemblyLoaderSteps
    {
        [Given(@"The assemblies loader used default assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedDefaultAssemblyLoadingStrategy()
        {
            AssemblyLoader.LoadAssembliesFromPaths = AssemblyLoader.DefaultLoader;
        }

        [Given(@"The assemblies loader used custom assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedCustomAssemblyLoadingStrategy()
        {
            AssemblyLoader.LoadAssembliesFromPaths = RuntimeAssemblyLoader.Get;
        }
    }
}
