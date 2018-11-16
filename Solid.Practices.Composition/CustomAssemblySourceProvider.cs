namespace Solid.Practices.Composition
{
    /// <summary>
    /// Allows injecting custom namespaces to retrieve list of the assemblies to be 
    /// inspected for application elements,
    /// </summary>
    /// <seealso cref="AssemblySourceProviderBase" />
    public sealed class CustomAssemblySourceProvider : AssemblySourceProviderBase
    {
        private readonly string[] _endings;

        public CustomAssemblySourceProvider(string rootPath, string[] endings) : base(rootPath)
        {
            _endings = endings;
        }

        protected override string[] ResolveNamespaces() => _endings;
    }
}