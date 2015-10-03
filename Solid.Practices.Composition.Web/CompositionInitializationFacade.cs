namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Composition initialization facade for server part of web applications
    /// </summary>
    public class CompositionInitializationFacade : CompositionInitializationFacadeBase
    {
        protected override IAssembliesReadOnlyResolver CreateAssembliesResolver()
        {
            return new AssembliesResolver(CompositionContainer);
        }
    }
}
