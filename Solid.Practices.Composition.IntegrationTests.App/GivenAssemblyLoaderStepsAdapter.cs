using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class GivenAssemblyLoaderStepsAdapter
    {
        [Given(@"The assemblies loader used default assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedDefaultAssemblyLoadingStrategy()
        {
            //for readability - consider explicitly setting the method - extract it within the assembly loader
        }

        [Given(@"The assemblies loader used custom assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedCustomAssemblyLoadingStrategy()
        {
            AssemblyLoader.LoadAssembliesFromPaths = DynamicLoader.LoadAssemblies;
        }
    }
}
