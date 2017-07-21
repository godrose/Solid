using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Solid.Practices.Modularity
{
    /// <summary>
    /// Middleware that's able to register composition modules into the IoC container.
    /// </summary>
    /// <typeparam name="TIocContainer">The type of the IoC  container.</typeparam>
    /// <typeparam name="TContainerConstraint">The type of the IoC container constraint.</typeparam>
    /// <seealso cref="Middleware.IMiddleware{TIocContainer}" />
    public class ContainerRegistrationMiddleware<TIocContainer, TContainerConstraint> : IMiddleware<TIocContainer>
        where TIocContainer : class, TContainerConstraint
    {
        private readonly IEnumerable<ICompositionModule> _modules;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ContainerRegistrationMiddleware{TIocContainer, TContainerConstraint}"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public ContainerRegistrationMiddleware(IEnumerable<ICompositionModule> modules)
        {
            _modules = modules;
        }

        /// <summary>
        /// Applies the middleware on the specified object.
        /// </summary>        
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public TIocContainer Apply(TIocContainer @object)
        {
            if (_modules != null)
            {
                foreach (var compositionModule in _modules.OfType<ICompositionModule<TContainerConstraint>>())
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
    public class ContainerPlainRegistrationMiddleware<TIocContainer> : IMiddleware<TIocContainer> where TIocContainer : class
    {
        private readonly IEnumerable<ICompositionModule> _modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerPlainRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public ContainerPlainRegistrationMiddleware(IEnumerable<ICompositionModule> modules)
        {
            _modules = modules;
        }

        /// <summary>
        /// Applies the middleware on the specified object.
        /// </summary>        
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public TIocContainer Apply(TIocContainer @object)
        {
            foreach (var plainCompositionModule in _modules.OfType<IPlainCompositionModule>())
            {
                plainCompositionModule.RegisterModule();
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
        IMiddleware<TIocContainer>
        where TIocContainer : class, IDependencyRegistratorScoped
    {
        private readonly IEnumerable<ICompositionModule> _modules;
        private readonly Func<object> _lifetimeScopeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerScopedRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The modules collection.</param>
        /// <param name="lifetimeScopeProvider">The lifetime scope provider.</param>
        public ContainerScopedRegistrationMiddleware(
            IEnumerable<ICompositionModule> modules,
            Func<object> lifetimeScopeProvider)
        {
            _modules = modules;
            _lifetimeScopeProvider = lifetimeScopeProvider;
        }

        /// <summary>
        /// Applies the middleware on the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public TIocContainer Apply(TIocContainer @object)
        {
            if (_modules != null)
            {
                foreach (var scopedModule in _modules.OfType<IScopedCompositionModule>())
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
        IMiddleware<TIocContainer> where TIocContainer : class
    {
        private readonly IEnumerable<ICompositionModule> _modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerHierarchicalRegistrationMiddleware{TIocContainer}"/> class.
        /// </summary>
        /// <param name="modules">The modules.</param>
        public ContainerHierarchicalRegistrationMiddleware(IEnumerable<ICompositionModule> modules)
        {
            _modules = modules;
        }

        /// <summary>
        /// Applies the middleware on the specified object.
        /// </summary>        
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public TIocContainer Apply(TIocContainer @object)
        {
            var hierarchicalModules = _modules.OfType<IHierarchicalCompositionModule<TIocContainer>>();
            foreach (var hierarchicalModule in hierarchicalModules)
            {
                hierarchicalModule.RegisterModules(@object, _modules);
            }
            return @object;
        }
    }
}
