using System.Reflection;
using BoDi;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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

        [When(@"I use '(.*)'")]
        public void WhenIUse(string containerName)
        {
            _scenarioDataStore.ContainerName = containerName;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer = new ObjectContainerAdapter(new ObjectContainer());
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer = new ServiceCollection();
                    break;
            }
        }

        [When(@"I use '(.*)' with default registration method")]
        public void WhenIUseWithDefaultRegistrationMethod(string containerName)
        {
            _scenarioDataStore.ContainerName = containerName;
            switch (containerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer = new ObjectContainerAdapter(new ObjectContainer());
                    RegistrationMethodContext.SetDefaultRegistrationMethod<IIocContainer>((dr, match) =>
                        dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer = new ServiceCollection();
                    RegistrationMethodContext.SetDefaultRegistrationMethod<IServiceCollection>((dr, match) =>
                        dr.AddSingleton(match.ServiceType, match.ImplementationType));
                    break;
            }
        }

        [When(@"I use registration by ending")]
        public void WhenIUseRegistrationByEnding()
        {
            switch (_scenarioDataStore.ContainerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByEnding("ScenarioDataStore"),
                        (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByEnding("ScenarioDataStore"),
                        (dr, match) => dr.AddSingleton(match.ServiceType, match.ImplementationType));
                    break;
            }
        }

        [When(@"I use registration by contract which is an interface")]
        public void WhenIUseRegistrationByContractWhichIsAnInterface()
        {
            switch (_scenarioDataStore.ContainerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByContract(typeof(IScenarioDataStore)),
                        (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByContract(typeof(IScenarioDataStore)),
                        (dr, match) => dr.AddSingleton(match.ServiceType, match.ImplementationType));
                    break;
            }
        }

        [When(@"I use registration by contract which is a class")]
        public void WhenIUseRegistrationByContractWhichIsAClass()
        {
            switch (_scenarioDataStore.ContainerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByContract(typeof(ObjectBase)),
                        (dr, match) => dr.RegisterSingleton(match.ServiceType, match.ImplementationType));
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer.RegisterImplementationsAsContracts(_scenarioDataStore.Assemblies,
                        a => a.FindTypesByContract(typeof(ObjectBase)),
                        (dr, match) => dr.AddSingleton(match.ServiceType, match.ImplementationType));
                    break;
            }
        }

        [When(@"I use automagical registration")]
        public void WhenIUseAutomagicalRegistration()
        {
            switch (_scenarioDataStore.ContainerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    _scenarioDataStore.IocContainer.RegisterAutomagically(_scenarioDataStore.ContractsAssembly,
                        _scenarioDataStore.ImplementationsAssembly);
                    break;
                case Consts.MicrosoftDiContainerName:
                    _scenarioDataStore.DiContainer.RegisterAutomagically(_scenarioDataStore.ContractsAssembly,
                        _scenarioDataStore.ImplementationsAssembly);
                    break;
            }
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
            var dependency = ResolveDependency<IDependency>();
            dependency.Should().BeOfType<Dependency>();
        }

        private void ScenarioDataStoreShouldBeResolvedSuccessfully()
        {
            var scenarioDataStore = ResolveDependency<IScenarioDataStore>();
            scenarioDataStore.Should().BeOfType<ScenarioDataStore>();
        }

        private TDependency ResolveDependency<TDependency>() where TDependency : class
        {
            TDependency dependency = default;
            switch (_scenarioDataStore.ContainerName)
            {
                case Consts.SpecFlowObjectContainerName:
                    dependency = _scenarioDataStore.IocContainer.Resolve<TDependency>();
                    break;
                case Consts.MicrosoftDiContainerName:
                    dependency = _scenarioDataStore.DiContainer.BuildServiceProvider().GetService<TDependency>();
                    break;
            }

            return dependency;
        }
    }
}