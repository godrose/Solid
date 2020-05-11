using Attest.Testing.Contracts;
using Attest.Testing.Core;
using BoDi;
using JetBrains.Annotations;
using Solid.Cli.Specs.Launcher;
using Solid.Common;
using Solid.IoC.Adapters.BoDi;
using TechTalk.SpecFlow;

namespace Solid.Cli.Specs.Steps
{
    [Binding, UsedImplicitly]
    internal sealed class LifecycleHook
    {
        private readonly ObjectContainerAdapter _iocContainer;
        private static ILifecycleService _lifecycleService;

        public LifecycleHook(ObjectContainer objectContainer) =>
            _iocContainer = new ObjectContainerAdapter(objectContainer);

        [BeforeTestRun, UsedImplicitly]
        public static void BeforeAllScenarios()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        [BeforeScenario, UsedImplicitly]
        public void BeforeScenario()
        {
            _iocContainer.Initialize();
            _lifecycleService = _iocContainer.Resolve<ILifecycleService>();
            _lifecycleService.Setup();
            _iocContainer.Setup();
        }

        [AfterScenario, UsedImplicitly]
        public void AfterScenario()
        {
            _iocContainer.Teardown();
        }

        [AfterTestRun, UsedImplicitly]
        public static void AfterAllScenarios()
        {
            _lifecycleService.Teardown();
        }
    }
}
