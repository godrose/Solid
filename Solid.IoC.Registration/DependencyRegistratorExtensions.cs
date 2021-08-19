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
        /// Registers matching dependencies in an auto-magical fashion.
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
    }
}
