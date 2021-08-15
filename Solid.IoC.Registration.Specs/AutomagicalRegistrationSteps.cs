using System.Linq;
using System.Reflection;
using BoDi;
using FluentAssertions;
using Solid.IoC.Adapters.BoDi;
using Solid.Ioc.Registration.Specs.Tests.Contracts;
using Solid.Ioc.Registration.Specs.Tests.Implementations;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    [Binding]
    internal class AutomagicalRegistrationSteps
    {
        private string[] _assembliesNames;
        private IIocContainer _container;

        [Given(@"There are valid implementations for all declared dependencies")]
        public void GivenThereAreValidImplementationsForAllDeclaredDependencies()
        {
            _assembliesNames = new[]
            {
                "Solid.IoC.Registration.Specs.Tests.Contracts.dll",
                "Solid.IoC.Registration.Specs.Tests.Implementations.dll"
            };
        }

        [When(@"I use registration by ending")]
        public void WhenIUseRegistrationByEnding()
        {
            var assemblies = _assembliesNames.Select(Assembly.LoadFrom);
            _container = new ObjectContainerAdapter(new ObjectContainer());
            _container.RegisterImplementationsAsContracts(assemblies,
                a => a.FindTypesByEnding("ScenarioDataStore"),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }


        [Then(@"All dependencies can be resolved successfully")]
        public void ThenAllDependenciesCanBeResolvedSuccessfully()
        {
            var scenarioDataStore = _container.Resolve(typeof(IScenarioDataStore));
            scenarioDataStore.Should().BeOfType<ScenarioDataStore>();
        }

    }
}