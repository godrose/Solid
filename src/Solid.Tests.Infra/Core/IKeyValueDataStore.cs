// ReSharper disable once CheckNamespace
namespace Attest.Testing.Context
{
    /// <summary>
    /// Represents a key-value data store.
    /// </summary>
    public interface IKeyValueDataStore
    {
        /// <summary>
        /// Returns <see typeref="true"/> if the specified key is mapped to a stored value,
        /// <see typeref="false"/> otherwise.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Returns value that is mapped to the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValueByKey<T>(string key);

        /// <summary>
        /// Stores the value while mapping it to the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key"></param>
        void SetValueByKey<T>(T value, string key);
    }
}