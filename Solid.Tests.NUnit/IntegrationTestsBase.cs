using System;
using NUnit.Framework;
using Solid.Fake.Core;
using Solid.Practices.IoC;

namespace Solid.Tests.NUnit
{
    public abstract class IntegrationTestsBase<TContainer, TFakeFactory, TRootObject, TBootstrapper> : 
        Core.IntegrationTestsBase<TContainer, TFakeFactory, TRootObject>     
        where TContainer : IIocContainer, new()
        where TFakeFactory : IFakeFactory, new() where TRootObject : class
    {        
        [SetUp]
        protected override void Setup()
        {
            SetupCore();
            SetupOverride();
        }

        [TearDown]
        protected override void TearDown()
        {
            TearDownCore();
            TearDownOverride();
        }

        private void SetupCore()
        {
            IocContainer = new TContainer();
            Activator.CreateInstance(typeof (TBootstrapper), IocContainer);            
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
