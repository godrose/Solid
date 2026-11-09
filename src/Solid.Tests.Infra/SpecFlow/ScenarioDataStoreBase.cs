using Attest.Testing.Context;
#if REQNROLL
using Reqnroll;
#else
using TechTalk.SpecFlow;
#endif

// ReSharper disable once CheckNamespace
#if REQNROLL
namespace Attest.Testing.Reqnroll
#else
namespace Attest.Testing.SpecFlow
#endif
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