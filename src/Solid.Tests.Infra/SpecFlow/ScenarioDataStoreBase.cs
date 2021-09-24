using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.SpecFlow
{
    /// <summary>
    /// Base class for scenario data stores in SpecFlow-based projects.
    /// It allows storing and retrieving values dynamically.
    /// </summary>
    public abstract class ScenarioDataStoreBase : Context.ScenarioDataStoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioDataStoreBase"/> class.
        /// </summary>
        /// <param name="scenarioContext"></param>
        protected ScenarioDataStoreBase(ScenarioContext scenarioContext)
            : base(new ScenarioContextKeyValueDataStoreAdapter(scenarioContext))
        {

        }
    }
}