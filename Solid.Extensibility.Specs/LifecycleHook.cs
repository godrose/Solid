using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    internal sealed class LifecycleHook
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public LifecycleHook(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            var callbacks = new List<string>();
            _scenarioContext.Add("callbacks", callbacks);
            var aspects = new List<IAspect>();
            _scenarioContext.Add("aspects", aspects);
        }
    }
}
