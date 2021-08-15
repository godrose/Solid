using System.Linq;
using System.Reflection;
using BoDi;
using FluentAssertions;
using Solid.IoC.Adapters.BoDi;
using Solid.IoC.Registration.Specs.Tests.Contracts;
using Solid.IoC.Registration.Specs.Tests.Implementations;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    [Binding]
    internal class AutomagicalRegistrationSteps
    {
        private string[] _assembliesNames;
        private readonly AutomagicalRegistrationScenarioDataStore _scenarioDataStore;

        public AutomagicalRegistrationSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new AutomagicalRegistrationScenarioDataStore(scenarioContext);
        }

        [Given(@"There are valid implementations for all declared dependencies")]
        public void GivenThereAreValidImplementationsForAllDeclaredDependencies()
        {
            _assembliesNames = new[]
            {
                "Solid.IoC.Registration.Specs.Tests.Contracts.dll",
                "Solid.IoC.Registration.Specs.Tests.Implementations.dll"
            };
            _scenarioDataStore.Assemblies = _assembliesNames.Select(Assembly.LoadFrom).ToArray();
        }

        [When(@"I use object container")]
        public void WhenIUseObjectContainer()
        {
            _scenarioDataStore.IocContainer = new ObjectContainerAdapter(new ObjectContainer());
        }

        [When(@"I use registration by ending")]
        public void WhenIUseRegistrationByEnding()
        {
            _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                a => a.FindTypesByEnding("ScenarioDataStore"),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [When(@"I use registration by contract")]
        public void WhenIUseRegistrationByContract()
        {
            _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                a => a.FindTypesByContract(typeof(IScenarioDataStore)),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [Then(@"All dependencies can be resolved successfully")]
        public void ThenAllDependenciesCanBeResolvedSuccessfully()
        {
            var scenarioDataStore = _scenarioDataStore.IocContainer.Resolve(typeof(IScenarioDataStore));
            scenarioDataStore.Should().BeOfType<ScenarioDataStore>();
        }
    }
}