using System.Text;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Solid.Practices.Middleware.Specs
{
    [Binding]
    internal sealed class MiddlewareStepsAdapter
    {
        //TODO: Use Container
        private readonly ScenarioContext _scenarioContext;

        public MiddlewareStepsAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"There are middlewares with internal dependencies only")]
        public void GivenThereAreMiddlewaresWithInternalDependenciesOnly()
        {
            var middlewares = new IMiddleware<StringBuilder>[] { new MiddlewareB(), new MiddlewareA(), new MiddlewareC() };
            _scenarioContext.Add("middlewares", middlewares);
        }

        [Given(@"There are middlewares with dependencies and without dependencies including explicit")]
        public void GivenThereAreMiddlewaresWithDependenciesAndWithoutDependenciesIncludingExplicit()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareB(), new MiddlewareA(), new IndependentExplicitMiddleware(), new MiddlewareC()};
            _scenarioContext.Add("middlewares", middlewares);
        }

        [Given(@"There are middlewares with dependencies and without dependencies including implicit")]
        public void GivenThereAreMiddlewaresWithDependenciesAndWithoutDependenciesIncludingImplicit()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareB(), new MiddlewareA(), new IndependentImplicitMiddleware(), new MiddlewareC()};
            _scenarioContext.Add("middlewares", middlewares);
        }

        [Given(@"There are middlewares with internal dependencies only specified by attributes")]
        public void GivenThereAreMiddlewaresWithInternalDependenciesOnlySpecifiedByAttributes()
        {
            var middlewares = new IMiddleware<StringBuilder>[]
                {new MiddlewareAttrB(), new MiddlewareAttrA(), new MiddlewareAttrC()};
            _scenarioContext.Add("middlewares", middlewares);
        }

        [When(@"The middlewares are applied")]
        public void WhenTheMiddlewaresAreApplied()
        {
            var subject = new StringBuilder();
            var middlewares = _scenarioContext.Get<IMiddleware<StringBuilder>[]>("middlewares");
            MiddlewareApplier.ApplyMiddlewares(subject, middlewares);
            _scenarioContext.Add("subject", subject);
        }

        [Then(@"The result should be '(.*)'")]
        public void ThenTheResultShouldBe(string expectedOrder)
        {
            var subject = _scenarioContext.Get<StringBuilder>("subject");
            var result = subject.ToString();
            result.Should().Be(expectedOrder);
        }
    }
}
