using Solid.Common;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Specs.Steps.Adapters
{
    [Binding]
    public sealed class GivenEnvironmentStepsAdapter
    {
        [Given(@"I run in \.NETStandard environment")]
        public void GivenIRunIn_NETStandardEnvironment()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }
    }
}
