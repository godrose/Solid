namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows injecting custom namespaces to retrieve list of the assemblies to be 
    /// inspected for application elements.
    /// </summary>
    /// <seealso cref="AssemblySourceProviderBase" />
    public sealed class CustomAssemblySourceProvider : AssemblySourceProviderBase
    {
        private readonly string[] _endings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAssemblySourceProvider"/> class.
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="endings"></param>
        public CustomAssemblySourceProvider(string rootPath, string[] endings) : base(rootPath)
        {
            _endings = endings;
        }

        /// <inheritdoc />
        protected override string[] ResolveNamespaces() => _endings;
    }
}