using Solid.Common;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.IntegrationTests.App
{
    [Binding]
    internal sealed class GivenEnvironmentStepsAdapter
    {
        [Given(@"I run in \.NETStandard environment")]
        public void GivenIRunIn_NETStandardEnvironment()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }
    }
}
