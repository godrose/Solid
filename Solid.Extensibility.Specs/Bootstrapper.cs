using Attest.Testing.Bootstrapping;
using Solid.Practices.Composition;
using Solid.Practices.IoC;

namespace Solid.Extensibility.Specs
{
    internal sealed class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper(IDependencyRegistrator dependencyRegistrator) : base(dependencyRegistrator)
        {
        }

        public override CompositionOptions CompositionOptions => new CompositionOptions
            {Prefixes = new[] {"Solid.Extensibility.Specs"}};
    }
}