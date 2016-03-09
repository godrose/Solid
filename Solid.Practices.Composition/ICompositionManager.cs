namespace Solid.Practices.Composition
{    
    /// <summary>
    /// Represents means of initializing composition from the given path
    /// </summary>
    public interface ICompositionManager : ICompositionModulesProvider
    {        
        /// <summary>
        /// Initializes composition modules from the provided path.
        /// </summary>
        /// <param name="modulesPath">Modules path</param>
        /// <param name="prefixes">Optional file name prefixes; 
        /// used for filtering potential assembly candidates</param>
        void Initialize(string modulesPath, string[] prefixes = null);
    }
}
