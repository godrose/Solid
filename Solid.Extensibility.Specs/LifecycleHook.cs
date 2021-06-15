using Attest.Testing.SpecFlow;
using BoDi;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Solid.Extensibility.Specs
{
    [Binding]
    internal sealed class LifecycleHook : LifecycleHookBase
    {
        public LifecycleHook(ObjectContainer objectContainer)
            :base(objectContainer)
        {
        }

        protected override void InitializeContainer(IIocContainer iocContainer)
        {
            new Startup(iocContainer).Initialize();
            iocContainer
                .AddInstance(iocContainer)
                .AddSingleton<ExtensibleByTypeObject>()
                .AddSingleton<AspectsScenarioDataStore>()
                .AddSingleton<MiddlewareTypesScenarioDataStore<ExtensibleByTypeObject>>()
                .AddSingleton<ExtensibilityByTypeAspectScenarioDataStore<ExtensibleByTypeObject>>();
        }

        [AfterTestRun]
        public new static void AfterAllScenarios()
        {
            LifecycleHookBase.AfterAllScenarios();
        }
    }
}
