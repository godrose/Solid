namespace Solid.Extensibility
{
    /// <summary>
    /// Represents an object which can use aspects.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public interface IHaveAspects<T>
    {
        /// <summary>
        /// Registers an aspect for further usage
        /// </summary>
        /// <param name="aspect">The aspect to be used</param>
        /// <returns>The object with aspects</returns>
        T UseAspect(IAspect aspect);
    }
}