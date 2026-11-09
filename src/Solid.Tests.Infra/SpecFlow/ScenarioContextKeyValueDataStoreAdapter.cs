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
    /// Implementation of <see cref="IKeyValueDataStore" /> using <see cref="ScenarioContext"/>.
    /// </summary>
    public class ScenarioContextKeyValueDataStoreAdapter : IKeyValueDataStore
    {
        private readonly ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of <see cref="ScenarioContextKeyValueDataStoreAdapter"/>
        /// </summary>
        /// <param name="scenarioContext"></param>
        public ScenarioContextKeyValueDataStoreAdapter(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return _scenarioContext.ContainsKey(key);
        }

        /// <inheritdoc />
        public T GetValueByKey<T>(string key)
        {
            return (T)_scenarioContext[key];
        }

        /// <inheritdoc />
        public void SetValueByKey<T>(T value, string key)
        {
            _scenarioContext[key] = value;
        }
    }
}