using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Solid.Extensibility.Tests
{
    public class AspectsWrapperTests
    {
        [Fact]
        public void Initialize_AspectHasBasicDependency_AspectsAreInitializedByDependency()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Basic");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new string[] { });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));
            var secondAspect = new Mock<IAspect>();
            secondAspect.SetupGet(t => t.Id).Returns("Dependent");
            secondAspect.SetupGet(t => t.Dependencies).Returns(new [] {"Basic"});
            secondAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(secondAspect.Object.Id));

            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object).UseAspect(secondAspect.Object);
            wrapper.Initialize();

            callbacks.Should().BeEquivalentTo(new []{"Basic", "Dependent"}, c => c.WithStrictOrdering());
        }

        [Fact]
        public void
            Initialize_AspectHasBasicDependenciesAtTheSameLevel_AspectsAreInitializedByDependency()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Basic");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new string[] { });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));
            var secondAspect = new Mock<IAspect>();
            secondAspect.SetupGet(t => t.Id).Returns("DependentB");
            secondAspect.SetupGet(t => t.Dependencies).Returns(new[] {"Basic"});
            secondAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(secondAspect.Object.Id));
            var thirdAspect = new Mock<IAspect>();
            thirdAspect.SetupGet(t => t.Id).Returns("DependentA");
            thirdAspect.SetupGet(t => t.Dependencies).Returns(new[] {"Basic"});
            thirdAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(thirdAspect.Object.Id));

            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object).UseAspect(secondAspect.Object).UseAspect(thirdAspect.Object);
            wrapper.Initialize();

            callbacks.Should().BeEquivalentTo(new[] {"Basic", "DependentB", "DependentA"}, c => c.WithStrictOrdering());
        }

        [Fact]
        public void
            Initialize_AspectsHaveChainedDependencies_AspectsAreInitializedByDependency()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Platform");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new string[] { });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));
            var secondAspect = new Mock<IAspect>();
            secondAspect.SetupGet(t => t.Id).Returns("Discovery");
            secondAspect.SetupGet(t => t.Dependencies).Returns(new[] { "Platform" });
            secondAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(secondAspect.Object.Id));
            var thirdAspect = new Mock<IAspect>();
            thirdAspect.SetupGet(t => t.Id).Returns("Modularity");
            thirdAspect.SetupGet(t => t.Dependencies).Returns(new[] { "Platform", "Discovery" });
            thirdAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(thirdAspect.Object.Id));
           
            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object).UseAspect(thirdAspect.Object).UseAspect(secondAspect.Object);
            wrapper.Initialize();

            callbacks.Should().BeEquivalentTo(new[] { "Platform", "Discovery", "Modularity" }, c => c.WithStrictOrdering());
        }

        [Fact]
        public void Initialize_AspectHasMissingDependency_ExceptionIsThrown()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Basic");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new [] { "Missing" });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));           

            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object);
            var exception = Record.Exception(() => wrapper.Initialize());

            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo("Missing dependency Missing");            
        }

        [Fact]
        public void Initialize_AspectDependenciesHaveCycle_ExceptionIsThrown()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Basic");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new[] { "Missing" });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));
            var secondAspect = new Mock<IAspect>();
            secondAspect.SetupGet(t => t.Id).Returns("Missing");
            secondAspect.SetupGet(t => t.Dependencies).Returns(new[] { "Basic" });
            secondAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(secondAspect.Object.Id));

            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object).UseAspect(secondAspect.Object);
            var exception = Record.Exception(() => wrapper.Initialize());

            exception.Should().NotBeNull();
            exception.Message.Should().StartWith("Cyclic dependency");
        }

        [Fact]
        public void Initialize_AspectsHaveIdenticalIds_ExceptionIsThrown()
        {
            var callbacks = new List<string>();
            var firstAspect = new Mock<IAspect>();
            firstAspect.SetupGet(t => t.Id).Returns("Basic");
            firstAspect.SetupGet(t => t.Dependencies).Returns(new string[] { });
            firstAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(firstAspect.Object.Id));
            var secondAspect = new Mock<IAspect>();
            secondAspect.SetupGet(t => t.Id).Returns("Basic");
            secondAspect.SetupGet(t => t.Dependencies).Returns(new string[] { });
            secondAspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(secondAspect.Object.Id));

            var wrapper = new AspectsWrapper();
            wrapper.UseAspect(firstAspect.Object).UseAspect(secondAspect.Object);
            var exception = Record.Exception(() => wrapper.Initialize());

            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo("Id must be unique - Basic");
        }
    }    
}
