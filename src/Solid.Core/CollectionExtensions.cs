using System;
using System.Collections.Generic;

namespace Solid.Core
{
    /// <summary>
    /// Extension methods for collections
    /// </summary>
    public static class CollectionExtensions
    {   
        /// <summary>
        /// Invokes the passed action for each one of the non-null collection elements.
        /// Gracefully returns for null collection case.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action to be invoked.</param>
        /// <typeparam name="T">The type of the element.</typeparam>
        public static void ForEachSafe<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) return;
            foreach (var item in collection)
            {
                action(item);
            }
        }    
        
        internal static string[] Patch(this string[] input)
        {
            return input == null || input.Length == 0 ? new[] { Consts.WildCard } : input;
        }
    }
}