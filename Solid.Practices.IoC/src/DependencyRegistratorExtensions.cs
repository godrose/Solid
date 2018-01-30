using System;
using System.Collections.Generic;

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
    }
}