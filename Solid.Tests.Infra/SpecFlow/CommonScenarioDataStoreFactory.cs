using System;
using BoDi;
using Solid.IoC.Adapters.BoDi;
using TechTalk.SpecFlow;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.SpecFlow
{
    public static class CommonScenarioDataStoreFactory
    {
        public static CommonScenarioDataStore<TRootObject> 
            CreateCommonScenarioDataStore<TRootObject>(
            ScenarioContext scenarioContext,
            ObjectContainer objectContainer,
            Func<TRootObject> rootObjectFactory)
        {
            var containerAdapter = new ObjectContainerAdapter(objectContainer);
            var commonScenarioDataStore =
                new CommonScenarioDataStore<TRootObject>(scenarioContext)
                {
                    RootObject = rootObjectFactory(),
                    IocContainer = containerAdapter
                };
            return commonScenarioDataStore;
        }
    }
}
