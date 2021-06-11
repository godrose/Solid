using System;
using System.Collections.Generic;
using System.Linq;
using Attest.Testing.SpecFlow;
using BoDi;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.IoC.Adapters.BoDi;
using Solid.Patterns.Builder;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    [UsedImplicitly]
    internal sealed class MiddlewareTypesSteps
    {
        private MiddlewaresReadOnlyCollection<ExtensibleByTypeObject> _middlewaresReadOnlyCollection;

        //TODO: Move to the data store
        private MiddlewareTypesWrapper<ExtensibleByTypeObject> _middlewareTypesWrapper;
        private readonly ObjectContainerAdapter _containerAdapter;

        public MiddlewareTypesSteps(ObjectContainer objectContainer)
        {
            _containerAdapter = new ObjectContainerAdapter(objectContainer);
        }

        [When(@"The middleware types wrapper is created")]
        [UsedImplicitly]
        public void WhenTheMiddlewareTypesWrapperIsCreated()
        {
            _middlewareTypesWrapper = new MiddlewareTypesWrapper<ExtensibleByTypeObject>(_containerAdapter);
        }

        [When(@"I use a creatable middleware by specifying its type only")]
        public void WhenIUseACreatableMiddlewareBySpecifyingItsTypeOnly()
        {
            _middlewareTypesWrapper.Use<CreatableMiddleware>();
        }

        [When(@"I ensure the middlewares are created")]
        public void WhenIEnsureTheMiddlewaresAreCreated()
        {
            _middlewaresReadOnlyCollection = _middlewareTypesWrapper.Build();
        }

        [Then(@"This middleware is created successfully")]
        public void ThenThisMiddlewareIsCreatedSuccessfully()
        {
            _middlewaresReadOnlyCollection
                .Middlewares
                .Should()
                .ContainSingle(t => t is CreatableMiddleware);
        }
    }

    internal class MiddlewareTypesWrapper<TExtensible> :
        IBuilder<MiddlewaresReadOnlyCollection<TExtensible>>
        where TExtensible : class
    {
        private readonly IIocContainer _iocContainer;
        private readonly List<Type> _middlewareTypes = new List<Type>();

        public MiddlewareTypesWrapper(
            IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;
        }

        public MiddlewaresReadOnlyCollection<TExtensible> Build()
        {
            return new MiddlewaresReadOnlyCollection<TExtensible>(
                _middlewareTypes.Select(t => (IMiddleware<TExtensible>) _iocContainer.Resolve(t)));
        }

        public void Use<TExtension>() where TExtension : class, IMiddleware<TExtensible>
        {
            _middlewareTypes.Add(typeof(TExtension));
            _iocContainer.RegisterSingleton<TExtension>();
        }
    }

    internal class MiddlewaresReadOnlyCollection<TExtensible> where TExtensible : class
    {
        public MiddlewaresReadOnlyCollection(IEnumerable<IMiddleware<TExtensible>> middlewares)
        {
            Middlewares = middlewares.ToArray();
        }

        public IEnumerable<IMiddleware<TExtensible>> Middlewares { get; }
    }

    internal class MiddlewareTypeScenarioDataStore : ScenarioDataStoreBase
    {
        public MiddlewareTypeScenarioDataStore(ScenarioContext scenarioContext) :
            base(scenarioContext)
        {
        }
    }

    internal class ExtensibleByTypeObject
    {
    }

    internal class CreatableMiddleware : IMiddleware<ExtensibleByTypeObject>
    {
        public ExtensibleByTypeObject Apply(ExtensibleByTypeObject @object)
        {
            return @object;
        }
    }
}