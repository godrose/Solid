using FluentAssertions;
using Solid.Common;
using Xunit;

namespace Solid.Practices.Composition.Tests
{
    public class DiscoveryAspectTests
    {
        [Fact]
        public void Initialize_PrefixesAreSet_OnlyMatchingAssembliesAreLoaded()
        {
            var compositionOptions = new CompositionOptions
            {
                Prefixes = new [] {"Solid"}
            };
            PlatformProvider.Current = new NetStandardPlatformProvider();
            var discoveryAspect = new DiscoveryAspect(compositionOptions);
            discoveryAspect.Initialize();

            var assemblies = discoveryAspect.Assemblies;
            assemblies.Should().HaveCount(10);
        }
    }
}
