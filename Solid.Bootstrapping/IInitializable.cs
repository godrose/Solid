namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an initializable object.
    /// Used during application bootstrapping.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
    }
}