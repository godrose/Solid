using System.Text;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Solid.Practices.Middleware.Specs
{
    [Binding]
    internal sealed class MiddlewareSteps
    {
        private readonly MiddlewareScenarioDataStore _scenarioDataStore;

        public MiddlewareSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new MiddlewareScenarioDataStore(scenarioContext);
        }

        [Given(@"There are middlewares with internal dependencies only")]
        public void GivenThereAreMiddlewaresWithInternalDependenciesOnly()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareB(), new MiddlewareA(), new MiddlewareC() };
            _scenarioDataStore.Middlewares = middlewares;
        }

        [Given(@"There are middlewares with dependencies and without dependencies including explicit")]
        public void GivenThereAreMiddlewaresWithDependenciesAndWithoutDependenciesIncludingExplicit()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareB(), new MiddlewareA(), new IndependentExplicitMiddleware(), new MiddlewareC()};
            _scenarioDataStore.Middlewares = middlewares;
        }

        [Given(@"There are middlewares with dependencies and without dependencies including implicit")]
        public void GivenThereAreMiddlewaresWithDependenciesAndWithoutDependenciesIncludingImplicit()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareB(), new MiddlewareA(), new IndependentImplicitMiddleware(), new MiddlewareC()};
            _scenarioDataStore.Middlewares = middlewares;
        }

        [Given(@"There are middlewares with internal dependencies only specified by attributes")]
        public void GivenThereAreMiddlewaresWithInternalDependenciesOnlySpecifiedByAttributes()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareAttrB(), new MiddlewareAttrA(), new MiddlewareAttrC()};
            _scenarioDataStore.Middlewares = middlewares;
        }

        [When(@"The middlewares are applied")]
        public void WhenTheMiddlewaresAreApplied()
        {
            var subject = new StringBuilder();
            var middlewares = _scenarioDataStore.Middlewares;
            MiddlewareApplier.ApplyMiddlewares(subject, middlewares);
            _scenarioDataStore.Subject = subject;
        }

        [Then(@"The result should be '(.*)'")]
        public void ThenTheResultShouldBe(string expectedOrder)
        {
            var subject = _scenarioDataStore.Subject;
            var result = subject.ToString();
            result.Should().Be(expectedOrder);
        }
    }
}
