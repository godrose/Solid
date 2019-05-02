using System;
using System.Collections.Generic;
using System.Linq;

namespace Solid.Core
{
    public static class TopologicalSortExtensions
    {
        public static IEnumerable<TItem> SortTopologically<TItem, TKey>(
            this IEnumerable<TItem> items, 
            Func<TItem, IEnumerable<TKey>> extractDeps,
            Func<TItem, TKey> extractId)
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

        public static string ExtractId(this object arg)
        {
            if (arg is IIdentifiable identifiable)
            {
                return identifiable.Id;
            }
            var idAttributes = arg.GetType().GetCustomAttributes(typeof(IdAttribute), inherit: true).OfType<IdAttribute>();
            var idAttribute = idAttributes.FirstOrDefault();
            return idAttribute != null ? idAttribute.Id : arg.GetType().Name;
        }

        public static IEnumerable<string> ExtractDependencies(this object arg)
        {
            if (arg is IHaveDependencies haveDependencies)
            {
                return haveDependencies.Dependencies;
            }
            var depAttributes = arg.GetType().GetCustomAttributes(typeof(DependenciesAttribute), inherit: true).OfType<DependenciesAttribute>();
            var depAttribute = depAttributes.FirstOrDefault();
            return depAttribute != null ? depAttribute.Dependencies : new string[] { };
        }
    }
}
