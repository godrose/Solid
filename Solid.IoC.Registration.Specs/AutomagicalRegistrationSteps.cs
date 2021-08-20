using System.Reflection;
using BoDi;
using FluentAssertions;
using Solid.IoC.Adapters.BoDi;
using Solid.IoC.Registration.Specs.Tests.Contracts;
using Solid.IoC.Registration.Specs.Tests.Implementations;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.IoC.Registration.Specs
{
    [Binding]
    internal class AutomagicalRegistrationSteps
    {
        private readonly AutomagicalRegistrationScenarioDataStore _scenarioDataStore;

        public AutomagicalRegistrationSteps(ScenarioContext scenarioContext)
        {
            _scenarioDataStore = new AutomagicalRegistrationScenarioDataStore(scenarioContext);
        }

        [Given(@"There are valid implementations for all declared dependencies")]
        public void GivenThereAreValidImplementationsForAllDeclaredDependencies()
        {
            var contractsAssemblyName = "Solid.IoC.Registration.Specs.Tests.Contracts.dll";
            var implementationsAssemblyName = "Solid.IoC.Registration.Specs.Tests.Implementations.dll";
            var contractsAssembly = Assembly.LoadFrom(contractsAssemblyName);
            var implementationsAssembly = Assembly.LoadFrom(implementationsAssemblyName);
            _scenarioDataStore.ContractsAssembly = contractsAssembly;
            _scenarioDataStore.ImplementationsAssembly = implementationsAssembly;
            _scenarioDataStore.Assemblies = new[] {contractsAssembly, implementationsAssembly};
        }

        [When(@"I use object container")]
        public void WhenIUseObjectContainer()
        {
            _scenarioDataStore.IocContainer = new ObjectContainerAdapter(new ObjectContainer());
        }

        [When(@"I use object container with default registration method")]
        public void WhenIUseObjectContainerWithDefaultRegistrationMethod()
        {
            _scenarioDataStore.IocContainer = new ObjectContainerAdapter(new ObjectContainer());
            RegistrationMethodContext.SetDefaultRegistrationMethod<IIocContainer>((dr, match) =>
                dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [When(@"I use registration by ending")]
        public void WhenIUseRegistrationByEnding()
        {
            _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                a => a.FindTypesByEnding("ScenarioDataStore"),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [When(@"I use registration by contract which is an interface")]
        public void WhenIUseRegistrationByContractWhichIsAnInterface()
        {
            _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                a => a.FindTypesByContract(typeof(IScenarioDataStore)),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [When(@"I use registration by contract which is a class")]
        public void WhenIUseRegistrationByContractWhichIsAClass()
        {
            _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                a => a.FindTypesByContract(typeof(ObjectBase)),
                (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
        }

        [When(@"I use automagical registration")]
        public void WhenIUseAutomagicalRegistration()
        {
            _scenarioDataStore.IocContainer.RegisterAutomagically(_scenarioDataStore.ContractsAssembly,
                _scenarioDataStore.ImplementationsAssembly);
        }

        [Then(@"All dependencies by ending can be resolved successfully")]
        public void ThenAllDependenciesByEndingCanBeResolvedSuccessfully()
        {
            ScenarioDataStoreShouldBeResolvedSuccessfully();
        }

        [Then(@"All dependencies by contract which is an interface can be resolved successfully")]
        public void ThenAllDependenciesByContractWhichIsAnInterfaceCanBeResolvedSuccessfully()
        {
            ScenarioDataStoreShouldBeResolvedSuccessfully();
        }

        [Then(@"All dependencies by contract which is a class can be resolved successfully")]
        public void ThenAllDependenciesByContractWhichIsAClassCanBeResolvedSuccessfully()
        {
            ScenarioDataStoreShouldBeResolvedSuccessfully();
        }

        [Then(@"All dependencies can be resolved successfully")]
        public void ThenAllDependenciesCanBeResolvedSuccessfully()
        {
            ScenarioDataStoreShouldBeResolvedSuccessfully();
            var dependency = _scenarioDataStore.IocContainer.Resolve<IDependency>();
            dependency.Should().BeOfType<Dependency>();
        }

        private void ScenarioDataStoreShouldBeResolvedSuccessfully()
        {
            var scenarioDataStore = _scenarioDataStore.IocContainer.Resolve<IScenarioDataStore>();
            scenarioDataStore.Should().BeOfType<ScenarioDataStore>();
        }
    }
}