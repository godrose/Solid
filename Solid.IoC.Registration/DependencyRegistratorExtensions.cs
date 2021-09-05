using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.IoC.Registration
{
    /// <summary>
    /// The extension methods which facilitate registration phase.
    /// </summary>
    public static class DependencyRegistratorExtensions
    {
        /// <summary>
        /// Registers matching dependencies by their contracts in an auto-magical fashion.
        /// </summary>
        /// <typeparam name="TDependencyRegistrator">The type of the dependency registrator.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="assemblies">The list of assemblies to be inspected for dependency matches.</param>
        /// <param name="typeExtractionMethod">The strategy for extracting types from the list of assemblies.</param>
        /// <param name="registrationMethod">The way of registering matching dependencies into the provided registrator.</param>
        /// <returns></returns>
        public static TDependencyRegistrator RegisterImplementationsAsContracts<TDependencyRegistrator>(
            this TDependencyRegistrator dependencyRegistrator,
            IEnumerable<Assembly> assemblies,
            Func<IEnumerable<Assembly>, Type[]> typeExtractionMethod,
            Action<TDependencyRegistrator, TypeMatch> registrationMethod)
        {
            var assembliesArray = assemblies as Assembly[] ?? assemblies.ToArray();
            var implementationCandidates = typeExtractionMethod(assembliesArray).Where(t => t.IsClass);
            var matches = implementationCandidates.Select(BuildMatch).Where(t => t != null);

            foreach (var match in matches)
            {
                registrationMethod(dependencyRegistrator, match);
            }

            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers matching dependencies as themselves in an auto-magical fashion.
        /// </summary>
        /// <typeparam name="TDependencyRegistrator">The type of the dependency registrator.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="assemblies">The list of assemblies to be inspected for dependency matches.</param>
        /// <param name="typeExtractionMethod">The strategy for extracting types from the list of assemblies.</param>
        /// <param name="registrationMethod">The way of registering matching dependencies into the provided registrator.</param>
        /// <returns></returns>
        public static TDependencyRegistrator RegisterImplementations<TDependencyRegistrator>(
            this TDependencyRegistrator dependencyRegistrator,
            IEnumerable<Assembly> assemblies,
            Func<IEnumerable<Assembly>, Type[]> typeExtractionMethod,
            Action<TDependencyRegistrator, Type> registrationMethod)
        {
            var assembliesArray = assemblies as Assembly[] ?? assemblies.ToArray();
            var matches = typeExtractionMethod(assembliesArray).Where(t => t.IsClass);

            foreach (var match in matches)
            {
                registrationMethod(dependencyRegistrator, match);
            }

            return dependencyRegistrator;
        }

        private static TypeMatch BuildMatch(Type implementationCandidate)
        {
            var match = FindContractMatch(implementationCandidate);

            return match == null
                ? null
                : new TypeMatch(match, implementationCandidate);
        }

        private static Type FindContractMatch(Type implementationCandidate)
        {
            var contractName = $"I{implementationCandidate.Name}";
            return implementationCandidate
                .GetImplementedInterfaces()
                .FirstOrDefault(t => t.Name == contractName);
        }

        /// <summary>
        /// Registers types as their abstractions using provided registration method
        /// The assemblies are inspected using [IDependency]--[Dependency] naming convention
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="contractsAssembly">The assembly which contains the contracts/abstractions.</param>
        /// <param name="implementationsAssembly">The assembly which contains the implementations.</param>
        /// <param name="registrationMethod">The registration method.</param>
        /// <returns></returns>
        public static TDependencyRegistrator RegisterAutomagically<TDependencyRegistrator>(
            this TDependencyRegistrator dependencyRegistrator,
            Assembly contractsAssembly,
            Assembly implementationsAssembly,
            Action<TDependencyRegistrator, TypeMatch> registrationMethod = null)
        {
            registrationMethod = registrationMethod ?? RegistrationMethodContext.GetDefaultRegistrationMethod<TDependencyRegistrator>();
            var contracts = contractsAssembly.DefinedTypes
                .Where(t => t.IsInterface)
                .Select(t => t.AsType())
                .ToArray();
            var implementations =
                implementationsAssembly.DefinedTypes
                    .Where(t => t.IsInterface == false)
                    .ToArray();
            var contractsInfo = contracts.ToDictionary(t => t.Name, t => t);
            var implementationsInfo = implementations
                .Where(t => t.Name.StartsWith("<>") == false)
                .ToDictionary(t => t.Name, t => t);
            foreach (var implementationInfo in implementationsInfo)
            {
                contractsInfo.TryGetValue("I" + implementationInfo.Key, out Type match);
                if (match != null)
                {
                    registrationMethod.Invoke(dependencyRegistrator,                     
                        new TypeMatch(match, implementationInfo.Value.AsType()));
                }
            }
            return dependencyRegistrator;
        }
    }
}
