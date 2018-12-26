namespace Solid.Core
{
    /// <summary>
    /// Represents an object with identifiable dependencies   
    /// </summary>
    public interface IHaveDependencies
    {
        /// <summary>
        /// The collection of dependencies
        /// </summary>
        string[] Dependencies { get; }
    }
}