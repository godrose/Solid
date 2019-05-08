using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Core;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Base class for registration middlewares with modules.
    /// </summary>
    public abstract class RegistrationMiddlewareBase
    {
        /// <summary>
        /// The collection of modules
        /// </summary>
        protected readonly IEnumerable<ICompositionModule> Modules;

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationMiddlewareBase"/>
        /// </summary>
        /// <param name="modules">The composition modules.</param>
        protected RegistrationMiddlewareBase(IEnumerable<ICompositionModule> modules) => Modules = modules;
    }

    /// <summary>
    /// Middleware that's able to register composition modules into the IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC  container.</typeparam>
    /// <typeparam name="TContainerConstraint">The type of the IoC container constraint.</typeparam>
    /// <seealso cref="Middleware.IMiddleware{TIocContainer}" />
    public class ContainerRegistrationMiddleware<TIocContainer, TContainerConstraint> :
        RegistrationMiddlewareBase,
        IMiddleware<TIocContainer>
        where TIocContainer : class, TContainerConstraint
    {
        /// <summary>
        /// Creates a new instance of the 
        /// <see cref="ContainerRegistrationMiddleware{TIocContainer, TContainerConstraint}"/> class.
        /// </summary>
        /// <param name="modules">The composition modules.</param>
        public ContainerRegistrationMiddleware(IEnumerable<ICompositionModule> modules) : base(modules)
        {
        }

        /// <inheritdoc />        
        public TIocContainer Apply(TIocContainer @object)
        {
            if (Modules != null)
            {
                var matchingModules = Modules.OfType<ICompositionModule<TContainerConstraint>>();
                var modules =
                    matchingModules.SortTopologically();                
                foreach (var compositionModule in modules)
                {
                    compositionModule.RegisterModule(@object);
                }
            }

            return @object;
        }
    }

    /// <summary>
    /// Middleware that's able to register plain composition modules into the IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC container.</typeparam>
    /// <seealso cref="Middleware.IMiddleware{TIocContainer}" />
    public class ContainerPlainRegistrationMiddleware<TIocContainer> : 
        RegistrationMiddlewareBase,
        IMiddleware<TIocContainer> where TIocContainer : class
    {        
        /// <summary>
        /// Creates a new instance of the <see cref="ContainerPlainRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The composition modules.</param>
        public ContainerPlainRegistrationMiddleware(IEnumerable<ICompositionModule> modules) : base(modules)
        {
        }

        /// <inheritdoc />        
        public TIocContainer Apply(TIocContainer @object)
        {
            if (Modules != null)
            {
                var matchingModules = Modules.OfType<IPlainCompositionModule>();
                var modules =
                    matchingModules.SortTopologically();
                foreach (var plainCompositionModule in modules)
                {
                    plainCompositionModule.RegisterModule();
                }
            }            
            return @object;
        }
    }

    /// <summary>
    /// Middleware that's able to register scoped composition modules into the IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC container.</typeparam>
    /// <seealso />
    public class ContainerScopedRegistrationMiddleware<TIocContainer> :
        RegistrationMiddlewareBase,
        IMiddleware<TIocContainer>
        where TIocContainer : class, IDependencyRegistratorScoped
    {        
        private readonly Func<object> _lifetimeScopeProvider;

        /// <summary>
        /// Creates a new instance of the <see cref="ContainerScopedRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The composition modules.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        public ContainerScopedRegistrationMiddleware(
            IEnumerable<ICompositionModule> modules,
            Func<object> lifetimeScopeProvider)
            :base(modules) => _lifetimeScopeProvider = lifetimeScopeProvider;

        /// <inheritdoc />        
        public TIocContainer Apply(TIocContainer @object)
        {
            if (Modules != null)
            {
                foreach (var scopedModule in Modules.OfType<IScopedCompositionModule>())
                {
                    scopedModule.RegisterModule(@object, _lifetimeScopeProvider);
                }
            }
            return @object;
        }
    }

    /// <summary>
    /// Middleware that's able to register hierarchical composition modules into the IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC container.</typeparam>
    /// <seealso cref="Middleware.IMiddleware{TIocContainer}" />
    public class ContainerHierarchicalRegistrationMiddleware<TIocContainer> :
        RegistrationMiddlewareBase,
        IMiddleware<TIocContainer> where TIocContainer : class
    {        
        /// <summary>
        /// Creates a new instance of the <see cref="ContainerHierarchicalRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The composition modules.</param>
        public ContainerHierarchicalRegistrationMiddleware(IEnumerable<ICompositionModule> modules) : base(modules)
        {
        }

        /// <inheritdoc />        
        public TIocContainer Apply(TIocContainer @object)
        {
            if (Modules != null)
            {
                var matchingModules = Modules.OfType<IHierarchicalCompositionModule<TIocContainer>>();
                var modules =
                    matchingModules.SortTopologically();
                foreach (var hierarchicalModule in modules)
                {
                    hierarchicalModule.RegisterModules(@object, Modules);
                }
            }                        
            return @object;
        }
    }
}
