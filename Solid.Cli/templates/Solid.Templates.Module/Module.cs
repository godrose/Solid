using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Cli.Modularity
{
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
			//Add your registrations here
            //dependencyRegistrator
                //.AddSingleton<IContract, Implementation>();
        }
    }
}