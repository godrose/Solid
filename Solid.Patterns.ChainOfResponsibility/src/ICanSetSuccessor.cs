namespace Solid.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// Represents an object that can have successor.
    /// </summary>
    /// <typeparam name="TSuccessor">The type of the successor.</typeparam>
    public interface ICanSetSuccessor<TSuccessor>
    {
        /// <summary>
        /// Sets the successor.
        /// </summary>
        /// <param name="successor">The successor.</param>
        /// <returns></returns>
        TSuccessor SetSuccessor(TSuccessor successor);
    }
}