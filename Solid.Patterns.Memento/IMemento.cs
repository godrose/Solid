namespace Solid.Patterns.Memento
{
    /// <summary>
    /// Represents memento object, which is used for restoration of object state.
    /// </summary>
    /// <typeparam name="T">Type of restoration target.</typeparam>
    public interface IMemento<T>
    {
        /// <summary>
        /// Restores the object to its previous state.
        /// </summary>
        /// <param name="target">Restoration target</param>
        /// <returns>Memento that allows to execute action that's inverse to the restoration</returns>
        IMemento<T> Restore(T target);
    }
}
