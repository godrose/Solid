using System.Text;
using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace Solid.Practices.Middleware.Specs
{
    internal sealed class MiddlewareScenarioDataStore : ScenarioDataStoreBase
    {
        public MiddlewareScenarioDataStore(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        public IMiddleware<StringBuilder>[] Middlewares
        {
            get => GetValue<IMiddleware<StringBuilder>[]>();
            set => SetValue(value);
        }

        public StringBuilder Subject
        {
            get => GetValue<StringBuilder>();
            set => SetValue(value);
        }
    }
}
