namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows injecting custom namespaces to retrieve list of the assemblies to be 
    /// inspected for application elements.
    /// </summary>
    /// <seealso cref="AssemblySourceProviderBase" />
    public sealed class CustomAssemblySourceProvider : AssemblySourceProviderBase
    {
        private readonly string[] _namespaces;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAssemblySourceProvider"/> class.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        /// <param name="prefixes">The prefixes.</param>
        /// <param name="namespaces">The namespaces.</param>
        public CustomAssemblySourceProvider(
            string rootPath, 
            string[] prefixes = null, 
            string[] namespaces = null) 
            : base(rootPath, prefixes)
        {
            _namespaces = namespaces;
        }

        /// <inheritdoc />
        protected override string[] ResolveNamespaces() => _namespaces;
    }
}