using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Solid.Extensibility.Specs
{
    [Binding]
    internal sealed class AspectsStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public AspectsStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"The aspect is created with Id '(.*)' and Dependencies '(.*)'")]
        public void GivenTheAspectIsCreatedWithIdAndDependencies(string id, string depStr)
        {
            var deps = string.IsNullOrWhiteSpace(depStr) 
                ? new string[] { } 
                : depStr.Split(new[] {';'}).ToArray();
            var aspects = _scenarioContext.Get<List<IAspect>>("aspects");
            var callbacks = _scenarioContext.Get<List<string>>("callbacks");
            var aspect = new Mock<IAspect>();
            aspect.SetupGet(t => t.Id).Returns(id);
            aspect.SetupGet(t => t.Dependencies).Returns(deps);
            aspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(aspect.Object.Id));
            aspects.Add(aspect.Object);
        }

        [When(@"The aspects wrapper is created with the aspects and initialized")]
        public void WhenTheAspectsWrapperIsCreatedWithTheAspectsAndInitialized()
        {
            var aspects = _scenarioContext.Get<List<IAspect>>("aspects");
            var wrapper = new AspectsWrapper();
            foreach (var aspect in aspects)
            {
                wrapper.UseAspect(aspect);
            }
            var exception = Record.Exception(()=>  wrapper.Initialize());
            _scenarioContext.Add("exception", exception);
        }

        [Then(@"the aspects should be invoked in the following order '(.*)'")]
        public void ThenTheAspectsShouldBeInvokedInTheFollowingOrder(string aspectsInvocationStr)
        {
            var expectedOrder = string.IsNullOrWhiteSpace(aspectsInvocationStr)
                ? new string[] { }
                : aspectsInvocationStr.Split(new[] {';'}).ToArray();
            var callbacks = _scenarioContext.Get<List<string>>("callbacks");
            callbacks.Should().BeEquivalentTo(expectedOrder, c => c.WithStrictOrdering());
        }

        [Then(@"There should be an error with the following message '(.*)'")]
        public void ThenThereShouldBeAnErrorWithTheFollowingMessage(string expectedMessage)
        {
            var exception = _scenarioContext.Get<Exception>("exception");
            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo(expectedMessage);
        }
    }
}