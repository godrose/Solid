namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Composition initialization facade for server part of web applications
    /// </summary>
    public class CompositionInitializationFacade : CompositionInitializationFacadeBase
    {
        /// <summary>
        /// Creates the assemblies resolver.
        /// </summary>
        /// <returns></returns>
        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(CompositionContainer);
        }
    }
}
