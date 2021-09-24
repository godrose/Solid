using System.Linq;

namespace Solid.Practices.Composition.Client
{
    /// <summary>
    /// Retrieves list of the assemblies to be 
    /// inspected for application elements in the client side.
    /// </summary>
    /// <seealso cref="AssemblySourceProviderBase" />
    public class ClientAssemblySourceProvider : AssemblySourceProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientAssemblySourceProvider"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="prefixes">The prefixes.</param>
        public ClientAssemblySourceProvider(string rootPath, string[] prefixes) : base(rootPath, prefixes)
        {
        }

        /// <summary>
        /// Returns the list of namespaces to be looked for during assembly discovery.
        /// </summary>
        /// <returns></returns>
        protected override string[] ResolveNamespaces() => AssemblyLoadingManager.ClientNamespaces().ToArray();
    }
}