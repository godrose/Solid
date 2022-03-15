using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using BootstrapperBase = Attest.Testing.Bootstrapping.BootstrapperBase;

namespace Solid.Cli.Specs.Launcher
{
    internal sealed class Bootstrapper :
        BootstrapperBase,
        IExtensible<IHaveRegistrator>
    {
        private readonly ExtensibilityAspect<IHaveRegistrator> _registratorExtensibilityAspect;

        public Bootstrapper(IDependencyRegistrator dependencyRegistrator) : base(dependencyRegistrator)
        {
            _registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator>(this);
            UseAspect(_registratorExtensibilityAspect);
        }

        public IHaveRegistrator Use(IMiddleware<IHaveRegistrator> middleware) =>
            _registratorExtensibilityAspect.Use(middleware);

        public override CompositionOptions CompositionOptions => new CompositionOptions
        {
            Prefixes = new[]
            {
                "Solid.Cli.Specs.Tests.Infra"
            }
        };
    }
}
