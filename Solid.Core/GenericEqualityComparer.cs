using System;
using System.Collections.Generic;

namespace Solid.Core
{
    /// <summary>
    /// Generic implementation of <see cref="EqualityComparer{T}"/>
    /// </summary>
    /// <typeparam name="TItem">The type of the items to be compared</typeparam>
    /// <typeparam name="TKey">The type of the comparison key</typeparam>
    public class GenericEqualityComparer<TItem, TKey> : EqualityComparer<TItem>
    {
        private readonly Func<TItem, TKey> _keyExtractor;
        private readonly EqualityComparer<TKey> _keyComparer;

        /// <inheritdoc />
        public GenericEqualityComparer(Func<TItem, TKey> keyExtractor)
        {
            _keyExtractor = keyExtractor;
            _keyComparer = EqualityComparer<TKey>.Default;
        }

        /// <inheritdoc />
        public override bool Equals(TItem x, TItem y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return _keyComparer.Equals(_keyExtractor(x), _keyExtractor(y));
        }

        /// <inheritdoc />
        public override int GetHashCode(TItem obj)
        {
            return obj == null ? 0 : _keyComparer.GetHashCode(_keyExtractor(obj));
        }
    }
}