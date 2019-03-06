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
        /// <param name="prefixes">The prefixes.</param>
        public ServerAssemblySourceProvider(string rootPath, string[] prefixes) : base(rootPath, prefixes)
        {
        }

        /// <inheritdoc />        
        protected override string[] ResolveNamespaces() => AssemblyLoadingManager.ServerNamespaces().ToArray();
    }
}