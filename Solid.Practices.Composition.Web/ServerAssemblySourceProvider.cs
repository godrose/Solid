using System.Linq;

namespace Solid.Practices.Composition.Web
{
    public class ServerAssemblySourceProvider : AssemblySourceProviderBase
    {
        public ServerAssemblySourceProvider(string rootPath) : base(rootPath)
        {
        }

        protected override string[] ResolveNamespaces()
        {
            return AssemblyLoadingManager.ServerNamespaces().ToArray();
        }
    }
}