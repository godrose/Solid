using Attest.Testing.Context;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.SpecFlow
{
    /// <summary>
    /// Base class for scenario data stores in SpecFlow-based projects.
    /// It allows accessing scenario context via named properties.
    /// </summary>
    public abstract class ScenarioDataStoreBase : ContextDataStoreBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScenarioDataStoreBase"/> class.
        /// </summary>
        /// <param name="scenarioContext">The scenario context</param>
        protected ScenarioDataStoreBase(ScenarioContext scenarioContext)
            : base(new ScenarioContextKeyValueDataStoreAdapter(scenarioContext))
        {

        }
    }
}