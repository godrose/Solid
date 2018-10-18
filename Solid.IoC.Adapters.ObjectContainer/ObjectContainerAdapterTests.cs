using System.Linq;
using FluentAssertions;
using Solid.Practices.IoC;
using Xunit;

namespace Solid.IoC.Adapters.ObjectContainer
{
    public class ObjectContainerAdapterTests
    {
        [Fact]
        public void ResolveAll_CollectionOfTypesIsRegisteredByInterface_ColelctionOfImplementationsIsResolved()
        {
            var container = new ObjectContainerAdapter(new BoDi.ObjectContainer());
            container.RegisterCollection<IDependency>(new[]
                {typeof(DependencyA), typeof(DependencyB)}, true);            

            var dependencies = container.ResolveAll<IDependency>().ToArray();
            dependencies[0].Should().BeOfType<DependencyA>();
            dependencies[1].Should().BeOfType<DependencyB>();            
        }

        interface IDependency
        {
            
        }

        class DependencyA : IDependency
        {
            
        }

        class DependencyB : IDependency
        {
            
        }
    }
}
