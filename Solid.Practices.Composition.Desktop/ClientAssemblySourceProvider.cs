using System.Linq;

namespace Solid.Practices.Composition.Desktop
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
        public ClientAssemblySourceProvider(string rootPath) : base(rootPath)
        {
        }

        /// <summary>
        /// Returns the list of namespaces to be looked for during assembly discovery.
        /// </summary>
        /// <returns></returns>
        protected override string[] ResolveNamespaces()
        {
            return AssemblyLoadingManager.ClientNamespaces().ToArray();
        }
    }
}