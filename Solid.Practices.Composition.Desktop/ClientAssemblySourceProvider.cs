using System.Linq;

namespace Solid.Practices.Composition.Desktop
{
    public class ClientAssemblySourceProvider : AssemblySourceProviderBase
    {
        public ClientAssemblySourceProvider(string rootPath) : base(rootPath)
        {
        }

        protected override string[] ResolveNamespaces()
        {
            return AssemblyLoadingManager.ClientNamespaces().ToArray();
        }
    }
}