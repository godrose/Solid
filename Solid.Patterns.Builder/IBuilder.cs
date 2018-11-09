namespace Solid.Patterns.Builder
{
    /// <summary>
    /// Represents a builder object.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds a new instance of the product.
        /// </summary>
        /// <returns></returns>
        object Build();
    }

    /// <summary>
    /// Represents a builder object.
    /// </summary>
    /// <typeparam name="T">The type of the product.</typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Builds a new instance of the product.
        /// </summary>
        /// <returns></returns>
        T Build();
    }
}