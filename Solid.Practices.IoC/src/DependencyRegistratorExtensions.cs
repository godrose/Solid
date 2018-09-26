using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solid.Practices.IoC
{
    /// <summary>
    /// The dependency registration methods using fluent API.
    /// </summary>
    public static class DependencyRegistratorExtensions
    {
        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <returns></returns>
        public static IDependencyRegistrator AddTransient<TDependency, TImplementation>(this IDependencyRegistrator dependencyRegistrator) where TImplementation : class, TDependency
        {
            dependencyRegistrator.RegisterTransient<TDependency, TImplementation>();
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public static IDependencyRegistrator AddTransient<TDependency, TImplementation>(
            this IDependencyRegistrator dependencyRegistrator, Func<TImplementation> dependencyCreator)
            where TImplementation : class, TDependency
        {
            dependencyRegistrator.RegisterTransient(dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        public static IDependencyRegistrator AddTransient<TDependency>(this IDependencyRegistrator dependencyRegistrator) where TDependency : class
        {
            dependencyRegistrator.RegisterTransient<TDependency>();
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public static IDependencyRegistrator AddTransient<TDependency>(this IDependencyRegistrator dependencyRegistrator,
            Func<TDependency> dependencyCreator) where TDependency : class
        {
            dependencyRegistrator.RegisterTransient(dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        public static IDependencyRegistrator AddTransient(this IDependencyRegistrator dependencyRegistrator, Type serviceType, Type implementationType)
        {
            dependencyRegistrator.RegisterTransient(serviceType, implementationType);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency in a transient lifetime style using fluent API.
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public static IDependencyRegistrator AddTransient(this IDependencyRegistrator dependencyRegistrator, Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            dependencyRegistrator.RegisterTransient(serviceType, implementationType, dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton using fluent API.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        public static IDependencyRegistrator AddSingleton<TDependency>(this IDependencyRegistrator dependencyRegistrator)
            where TDependency : class
        {
            dependencyRegistrator.RegisterSingleton<TDependency>();
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public static IDependencyRegistrator AddSingleton<TDependency>(this IDependencyRegistrator dependencyRegistrator,
            Func<TDependency> dependencyCreator) where TDependency : class
        {
            dependencyRegistrator.RegisterSingleton(dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        public static IDependencyRegistrator AddSingleton<TDependency, TImplementation>(this IDependencyRegistrator dependencyRegistrator) where TImplementation : class, TDependency
        {
            dependencyRegistrator.RegisterSingleton<TDependency, TImplementation>();
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency declaration.</typeparam>
        /// <typeparam name="TImplementation">The type of the dependency implementation.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>
        public static IDependencyRegistrator AddSingleton<TDependency, TImplementation>(
            this IDependencyRegistrator dependencyRegistrator, Func<TImplementation> dependencyCreator)
            where TImplementation : class, TDependency
        {
            dependencyRegistrator.RegisterSingleton<TDependency, TImplementation>(dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>   
        /// <param name="dependencyRegistrator">The dependency registrator.</param>     
        public static IDependencyRegistrator AddSingleton(this IDependencyRegistrator dependencyRegistrator, Type serviceType, Type implementationType)
        {
            dependencyRegistrator.RegisterSingleton(serviceType, implementationType);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers dependency as a singleton.
        /// </summary>
        /// <param name="serviceType">The type of the dependency declaration.</param>
        /// <param name="implementationType">The type of the dependency implementation.</param>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyCreator">Dependency creator delegate.</param>      
        public static IDependencyRegistrator AddSingleton(this IDependencyRegistrator dependencyRegistrator, Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            dependencyRegistrator.RegisterSingleton(serviceType, implementationType, dependencyCreator);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the instance of the dependency.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="instance">The instance of the dependency.</param>
        public static IDependencyRegistrator AddInstance<TDependency>(this IDependencyRegistrator dependencyRegistrator,
            TDependency instance) where TDependency : class
        {
            dependencyRegistrator.RegisterInstance(instance);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the instance of the dependency.v
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyType">The type of dependency.</param>
        /// <param name="instance">The instance of dependency.</param>
        public static IDependencyRegistrator AddInstance(this IDependencyRegistrator dependencyRegistrator, Type dependencyType,
            object instance)
        {
            dependencyRegistrator.RegisterInstance(dependencyType, instance);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        public static IDependencyRegistrator AddCollection<TDependency>(this IDependencyRegistrator dependencyRegistrator, IEnumerable<Type> dependencyTypes) where TDependency : class
        {
            dependencyRegistrator.RegisterCollection<TDependency>(dependencyTypes);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>
        /// <typeparam name="TDependency">The type of the dependency.</typeparam>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencies">The dependencies.</param>
        public static IDependencyRegistrator AddCollection<TDependency>(this IDependencyRegistrator dependencyRegistrator, IEnumerable<TDependency> dependencies)
            where TDependency : class
        {
            dependencyRegistrator.RegisterCollection(dependencies);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyType">The type of the dependency.</param>
        /// <param name="dependencyTypes">The dependency types.</param>
        public static IDependencyRegistrator AddCollection(this IDependencyRegistrator dependencyRegistrator, Type dependencyType,
            IEnumerable<Type> dependencyTypes)
        {
            dependencyRegistrator.RegisterCollection(dependencyType, dependencyTypes);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers the collection of the dependencies.
        /// </summary>        
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="dependencyType">The type of the dependency.</param>
        /// <param name="dependencies">The dependencies.</param>
        public static IDependencyRegistrator AddCollection(this IDependencyRegistrator dependencyRegistrator, Type dependencyType, IEnumerable<object> dependencies)
        {
            dependencyRegistrator.RegisterCollection(dependencyType, dependencies);
            return dependencyRegistrator;
        }

        /// <summary>
        /// Registers types as their abstractions using singleton lifetime style
        /// The assemblies are inspected using [IDependency]--[Dependency] naming convention
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="contractsAssembly">The assembly which contains the contracts/abstractions</param>
        /// <param name="implementationsAssembly">The assembly which contains the implementations.</param>
        /// <returns></returns>
        public static IDependencyRegistrator RegisterAutomagically(
            this IDependencyRegistrator dependencyRegistrator,
            Assembly contractsAssembly,
            Assembly implementationsAssembly)
        {
            return dependencyRegistrator.RegisterAutomagically(
                (d, serviceType, implementationType) => d.RegisterSingleton(serviceType, implementationType),
                contractsAssembly, 
                implementationsAssembly);
        }

        /// <summary>
        /// Registers types as their abstractions using provided registration method
        /// The assemblies are inspected using [IDependency]--[Dependency] naming convention
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="registrationMethod">The registration method.</param>
        /// <param name="contractsAssembly">The assembly which contains the contracts/abstractions.</param>
        /// <param name="implementationsAssembly">The assembly which contains the implementations.</param>
        /// <returns></returns>
        public static TDependencyRegistrator RegisterAutomagically<TDependencyRegistrator>(
            this TDependencyRegistrator dependencyRegistrator,
            Action<TDependencyRegistrator, Type, Type> registrationMethod,
            Assembly contractsAssembly,
            Assembly implementationsAssembly)
        {
            var contracts =
                contractsAssembly.DefinedTypes.Where(t => t.IsInterface).Select(t => t.AsType()).ToArray();
            var implementations =
                implementationsAssembly.DefinedTypes.Where(
                        t => t.IsInterface == false)
                    .ToArray();
            var contractsInfo = contracts.ToDictionary(t => t.Name, t => t);
            var implementationsInfo = implementations.Where(t => t.Name.StartsWith("<>") == false)
                .ToDictionary(t => t.Name, t => t);
            foreach (var implementationInfo in implementationsInfo)
            {
                contractsInfo.TryGetValue("I" + implementationInfo.Key, out Type match);
                if (match != null)
                {
                    registrationMethod.Invoke(dependencyRegistrator, match, implementationInfo.Value.AsType());
                }
            }
            return dependencyRegistrator;
        }
    }
}