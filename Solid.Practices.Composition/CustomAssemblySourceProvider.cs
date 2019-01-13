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
        /// <param name="rootPath"></param>
        /// <param name="namespaces"></param>
        public CustomAssemblySourceProvider(string rootPath, string[] namespaces) : base(rootPath)
        {
            _namespaces = namespaces;
        }

        /// <inheritdoc />
        protected override string[] ResolveNamespaces() => _namespaces;
    }
}