using Solid.Fake.Builders;
using Solid.Fake.Core;
using Solid.Practices.IoC;
using Solid.Tests.Core;
using TechTalk.SpecFlow;

namespace Solid.Tests.Specflow
{
    public abstract class IntegrationTestsBase<TContainer, TFakeFactory, TRootObject> :
        Solid.Tests.Core.IntegrationTestsBase<TContainer, TFakeFactory, TRootObject>
        where TContainer : IIocContainer, new()
        where TFakeFactory : IFakeFactory, new()
        where TRootObject : class
    {
        protected TContainer IocContainer;

        [BeforeScenario]
        protected override void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        [AfterScenario]
        protected override void TearDown()
        {
            TearDownCore();
            TearDownOverride();
        }

        private void SetupCore()
        {
            IocContainer = new TContainer();
            new TestBootstrapper<TContainer>(IocContainer);
        }

        protected virtual void SetupOverride()
        {

        }

        private void TearDownCore()
        {
            //Dispose();
        }

        protected virtual void TearDownOverride()
        {

        }        
    }
}
