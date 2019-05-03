using System;
using System.Collections.Generic;
using System.Linq;

namespace Solid.Core
{
    /// <summary>
    /// Extension methods for topological sort
    /// </summary>
    public static class TopologicalSortExtensions
    {
        /// <summary>
        /// Sorts the items topologically
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <typeparam name="TId">The type of the identity.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="extractDeps">The means for extracting the deps.</param>
        /// <param name="extractId">The means for extracting the id.</param>
        /// <returns></returns>
        public static IEnumerable<TItem> SortTopologically<TItem, TId>(
            this IEnumerable<TItem> items, 
            Func<TItem, IEnumerable<TId>> extractDeps,
            Func<TItem, TId> extractId)
        {
            const string sameKeyPrefix = "An item with the same key has already been added. Key: ";
            try
            {                
                var sortedItems = TopologicalSort.Sort(items, extractDeps, extractId, ignoreCycles: false);                             
                return sortedItems;
            }
            catch (ArgumentException e)
            {
                if (e.Message.StartsWith(sameKeyPrefix))
                {
                    throw new Exception($"Id must be unique - {e.Message.Substring(sameKeyPrefix.Length)}");
                }

                throw;
            }
            catch (KeyNotFoundException e)
            {
                var parts = e.Message.Split('\'');
                //TODO: Use RegEx
                if (parts.Length == 3)
                {
                    throw new Exception($"Missing dependency {parts[1]}");
                }
                throw;
            }
        }

        /// <summary>
        /// Extracts id from the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public static string ExtractId(this object @object)
        {
            if (@object is IIdentifiable identifiable)
            {
                return identifiable.Id;
            }
            var idAttributes = @object.GetType().GetCustomAttributes(typeof(IdAttribute), inherit: true).OfType<IdAttribute>();
            var idAttribute = idAttributes.FirstOrDefault();
            return idAttribute != null ? idAttribute.Id : @object.GetType().FullName;
        }

        /// <summary>
        /// Extracts deps from the specified object.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public static IEnumerable<string> ExtractDependencies(this object @object)
        {
            if (@object is IHaveDependencies haveDependencies)
            {
                return haveDependencies.Dependencies;
            }
            var depAttributes = @object.GetType().GetCustomAttributes(typeof(DependenciesAttribute), inherit: true).OfType<DependenciesAttribute>();
            var depAttribute = depAttributes.FirstOrDefault();
            return depAttribute != null ? depAttribute.Dependencies : new string[] { };
        }
    }
}
