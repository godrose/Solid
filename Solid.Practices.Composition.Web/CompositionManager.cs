namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Composition initialization facade for server part of applications
    /// </summary>
    public class CompositionManager : Composition.CompositionManager
    {
        private readonly string _rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>        
        /// <param name="rootPath">The root path.</param>
        public CompositionManager(string rootPath)
        {
            _rootPath = rootPath;
        }

        /// <summary>
        /// Creates the assemblies resolver.
        /// </summary>
        /// <returns></returns>
        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(new ServerAssemblySourceProvider(_rootPath));
        }
    }
}
