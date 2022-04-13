using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Attest.Testing.Context
{
    /// <summary>
    /// Base class for context data stores.
    /// It allows accessing context-specific data via named properties.
    /// </summary>
    public abstract class ContextDataStoreBase
    {
        private static readonly string MissingKey = string.Empty;

        private readonly IKeyValueDataStore _keyValueDataStore;

        /// <summary>
        /// Creates an instance of <see cref="ContextDataStoreBase"/>
        /// </summary>
        /// <param name="keyValueDataStore"></param>
        protected ContextDataStoreBase(IKeyValueDataStore keyValueDataStore)
        {
            _keyValueDataStore = keyValueDataStore;
        }

        /// <summary>
        /// Gets stored value by the specified key.
        /// Returns the specified default value if the value cannot be found using the specified key.
        /// If no default value is specified then the default value for the type is returned.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected T GetValue<T>(T defaultValue = default, [CallerMemberName] string key = default)
        {
            var coercedKey = Coerce(key);
            if (coercedKey == MissingKey)
            {
                return default;
            }
            var containsKey = _keyValueDataStore.ContainsKey(coercedKey);
            if (containsKey == false)
            {
                _keyValueDataStore.SetValueByKey(defaultValue, key);
            }

            return _keyValueDataStore.GetValueByKey<T>(coercedKey);
        }

        /// <summary>
        /// Sets value using the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="key">The key.</param>
        protected void SetValue<T>(T value, [CallerMemberName] string key = default)
        {
            var coercedKey = Coerce(key);
            if (coercedKey == MissingKey)
            {
                return;
            }
            _keyValueDataStore.SetValueByKey(value, coercedKey);
        }

        private static string Coerce(string key)
        {
            return key ?? MissingKey;
        }
    }
}