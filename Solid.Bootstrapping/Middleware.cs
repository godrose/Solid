using System;
using System.Linq;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Registers collection of services. This is used in case of 
    /// loosely coupled modular application where the services are defined in separate assemblies 
    /// and/or are otherwise private.
    /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>   
    /// <typeparam name="TService">The type of the service.</typeparam> 
    /// </summary>
    public sealed class RegisterCollectionMiddleware<TBootstrapper, TService> :
        IMiddleware<TBootstrapper>
        where TBootstrapper : class, IHaveRegistrator, IAssemblySourceProvider where TService : class
    {
        private readonly AssemblyOptions _options;

        /// <summary>
        /// Creates an instance of <see cref="RegisterCollectionMiddleware{TBootstrapper,TService}"/>
        /// </summary>
        /// <param name="options"></param>
        public RegisterCollectionMiddleware(AssemblyOptions options = null)
        {
            _options = options;
        }

        /// <inheritdoc />       
        public TBootstrapper
            Apply(TBootstrapper @object)
        {            
            var assemblies = @object.Assemblies.GetAssemblies(_options);            
            @object.Registrator.RegisterCollection<TService>(
                assemblies.Select(t => t.DefinedTypes.ToArray()).SelectMany(k => k).Select(t => t.AsType()),
                true);
            return @object;
        }
    }

    /// <summary>
    /// Registers collection of services. This is used in case of 
    /// loosely coupled modular application where the services are defined in separate assemblies 
    /// and/or are otherwise private.
    /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>    
    /// </summary>
    public class RegisterCollectionMiddleware<TBootstrapper> :
        IMiddleware<TBootstrapper>
        where TBootstrapper : class, IHaveRegistrator, IAssemblySourceProvider
    {
        private readonly Type _serviceContractType;
        private readonly AssemblyOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterCollectionMiddleware{TBootstrapper}"/> class.
        /// </summary>
        /// <param name="serviceContractType">The type of the module root object.</param>
        /// <param name="options"></param>
        public RegisterCollectionMiddleware(Type serviceContractType, AssemblyOptions options = null)
        {
            _serviceContractType = serviceContractType;
            _options = options;
        }

        /// <inheritdoc />       
        public TBootstrapper
            Apply(TBootstrapper @object)
        {            
            var assemblies = @object.Assemblies.GetAssemblies(_options);         
            @object.Registrator.RegisterCollection(_serviceContractType,
                assemblies.Select(t => t.DefinedTypes.ToArray()).SelectMany(k => k).Select(t => t.AsType()));
            return @object;
        }        
    }

    /// <summary>
    /// Registers modules.
    /// </summary>
    /// <typeparam name="TBootstrapper"></typeparam>
    public sealed class ModulesRegistrationMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, ICompositionModulesProvider, IHaveRegistrator
    {
        /// <inheritdoc />
        public TBootstrapper Apply(TBootstrapper @object)
        {
            var middlewares = new[]
            {
                new ContainerRegistrationMiddleware<IDependencyRegistrator, IDependencyRegistrator>(@object.Modules)
            };
            MiddlewareApplier.ApplyMiddlewares(@object.Registrator, middlewares);
            return @object;
        }
    }

    /// <summary>
    /// Registers the dependency resolver.
    /// </summary>
    /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
    /// <seealso cref="Solid.Practices.Middleware.IMiddleware{TBootstrapper}" />
    public class RegisterResolverMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, IHaveRegistrator
    {
        private readonly IDependencyResolver _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterResolverMiddleware{TBootstrapper}"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public RegisterResolverMiddleware(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        /// <inheritdoc />       
        public TBootstrapper Apply(TBootstrapper @object)
        {
            @object.Registrator.RegisterInstance(_resolver);
            return @object;
        }
    }
}
