namespace Solid.Practices.Composition.Contracts
{    
    /// <summary>
    /// Allows initializing composition from the given path.
    /// </summary>
    public interface ICompositionManager : ICompositionModulesProvider
    {        
        /// <summary>
        /// Initializes composition modules from the provided path.
        /// </summary>
        /// <param name="rootPath">Root path.</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates.</param>
        void Initialize(string rootPath, string[] prefixes = null);
    }
}
