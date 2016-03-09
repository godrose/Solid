using System.Linq;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Retrieves list of the assemblies to be 
    /// inspected for application elements in the server side.
    /// </summary>
    public class ServerAssemblySourceProvider : AssemblySourceProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAssemblySourceProvider"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public ServerAssemblySourceProvider(string rootPath) : base(rootPath)
        {
        }

        /// <summary>
        /// Returns the list of namespaces to be looked for during assembly discovery.
        /// </summary>
        /// <returns></returns>
        protected override string[] ResolveNamespaces()
        {
            return AssemblyLoadingManager.ServerNamespaces().ToArray();
        }
    }
}