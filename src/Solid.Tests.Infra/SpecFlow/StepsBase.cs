using System;
#if REQNROLL
using Attest.Testing.Reqnroll;
#else
using Attest.Testing.SpecFlow;
#endif
#if REQNROLL
using Reqnroll.BoDi;
#else
using BoDi;
#endif
#if REQNROLL
using Reqnroll;
#else
using TechTalk.SpecFlow;
#endif

#if REQNROLL
namespace Solid.Tests.Infra.Reqnroll
#else
namespace Solid.Tests.Infra.SpecFlow
#endif
{
    public abstract class StepsBase<TRootObject>
    {
        protected CommonScenarioDataStore<TRootObject> CommonScenarioDataStore { get; }

        protected StepsBase(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer,
            Func<TRootObject> rootObjectFactory)
        {
            CommonScenarioDataStore =
                CommonScenarioDataStoreFactory.CreateCommonScenarioDataStore(
                    scenarioContext,
                    objectContainer,
                    rootObjectFactory);
        }
    }
}
