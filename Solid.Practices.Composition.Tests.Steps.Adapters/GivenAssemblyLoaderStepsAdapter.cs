﻿using Common.Bootstrapping;
using Moq;
using Solid.Core;
using TechTalk.SpecFlow;

namespace Solid.Practices.Composition.Tests.Steps.Adapters
{
    [Binding]
    internal sealed class GivenAssemblyLoaderStepsAdapter
    {
        [Given(@"The assemblies loader used default assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedDefaultAssemblyLoadingStrategy()
        {
            AssemblyLoader.LoadAssembliesFromPaths = AssemblyLoader.DefaultLoader;
        }

        [Given(@"The assemblies loader used custom assembly loading strategy")]
        public void GivenTheAssembliesLoaderUsedCustomAssemblyLoadingStrategy()
        {
            var initializable = new Mock<IInitializable>();
            initializable.Object.UseDynamicLoad();
        }
    }
}
