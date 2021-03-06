﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using TechTalk.SpecFlow;
using Xunit;

namespace Solid.Extensibility.Specs
{
    [Binding]
    internal sealed class AspectsSteps
    {
        private readonly AspectsScenarioDataStore _aspectsScenarioDataStore;

        public AspectsSteps(ScenarioContext scenarioContext)
        {
            _aspectsScenarioDataStore = new AspectsScenarioDataStore(scenarioContext);
            _aspectsScenarioDataStore.Aspects = new List<IAspect>();
            _aspectsScenarioDataStore.Callbacks = new List<string>();
        }

        [Given(@"The aspect is created with Id '(.*)' and Dependencies '(.*)'")]
        public void GivenTheAspectIsCreatedWithIdAndDependencies(string id, string depStr)
        {
            var deps = string.IsNullOrWhiteSpace(depStr) 
                ? new string[] { } 
                : depStr.Split(new[] {';'}).ToArray();
            var aspects = _aspectsScenarioDataStore.Aspects;
            var callbacks = _aspectsScenarioDataStore.Callbacks;
            var aspect = new Mock<IAspect>();
            aspect.SetupGet(t => t.Id).Returns(id);
            aspect.SetupGet(t => t.Dependencies).Returns(deps);
            aspect.Setup(t => t.Initialize()).Callback(() => callbacks.Add(aspect.Object.Id));
            aspects.Add(aspect.Object);
        }

        [When(@"The aspects wrapper is created with the aspects and initialized")]
        public void WhenTheAspectsWrapperIsCreatedWithTheAspectsAndInitialized()
        {
            var aspects = _aspectsScenarioDataStore.Aspects;
            var wrapper = new AspectsWrapper();
            foreach (var aspect in aspects)
            {
                wrapper.UseAspect(aspect);
            }
            var exception = Record.Exception(()=>  wrapper.Initialize());
            _aspectsScenarioDataStore.Error = exception;
        }

        [Then(@"the aspects should be invoked in the following order '(.*)'")]
        public void ThenTheAspectsShouldBeInvokedInTheFollowingOrder(string aspectsInvocationStr)
        {
            var expectedOrder = string.IsNullOrWhiteSpace(aspectsInvocationStr)
                ? new string[] { }
                : aspectsInvocationStr.Split(new[] {';'}).ToArray();
            var callbacks = _aspectsScenarioDataStore.Callbacks;
            callbacks.Should().BeEquivalentTo(expectedOrder, c => c.WithStrictOrdering());
        }

        [Then(@"There should be an error with the following message '(.*)'")]
        public void ThenThereShouldBeAnErrorWithTheFollowingMessage(string expectedMessage)
        {
            var exception = _aspectsScenarioDataStore.Error;
            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo(expectedMessage);
        }
    }
}