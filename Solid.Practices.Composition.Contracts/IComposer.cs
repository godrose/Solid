namespace Solid.Practices.Composition.Contracts
{
    //TODO: Consider moving into ICompositionContainer
    /// <summary>
    /// Represents an object that is able to compose its contents.
    /// </summary>
    public interface IComposer
    {
        /// <summary>
        /// Composes the contents.
        /// </summary>
        void Compose();
    }
}
