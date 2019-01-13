//using excellent code from https://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Solid.Core
{
    /// <summary>
    /// Contains helpers for topological sort implementation
    /// </summary>
    public static class TopologicalSort
    {
        private static Func<T, IEnumerable<T>> RemapDependencies<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<TKey>> getDependencies, Func<T, TKey> getKey)
        {              
            var map = source.ToDictionary(getKey);
            return item =>
            {
                var dependencies = getDependencies(item);
                return dependencies?.Select(key => map[key]);
            };
        }

        /// <summary>
        /// Sorts the source items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="getKey"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<T> Sort<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<TKey>> getDependencies, Func<T, TKey> getKey, bool ignoreCycles = false)
        {
            ICollection<T> source2 = source as ICollection<T> ?? source.ToArray();
            return Sort<T>(source2, RemapDependencies(source2, getDependencies, getKey), null, ignoreCycles);
        }

        /// <summary>
        /// Sorts the source items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="getKey"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<T> Sort<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, Func<T, TKey> getKey, bool ignoreCycles = false)
        {
            return Sort<T>(source, getDependencies, new GenericEqualityComparer<T, TKey>(getKey), ignoreCycles);
        }

        /// <summary>
        /// Sorts the source items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="comparer"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null, bool ignoreCycles = false)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);

            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited, ignoreCycles);
            }

            return sorted;
        }
       
        private static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited, bool ignoreCycles)
        {
            var alreadyVisited = visited.TryGetValue(item, out var inProcess);

            if (alreadyVisited)
            {
                if (inProcess && !ignoreCycles)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency, getDependencies, sorted, visited, ignoreCycles);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }

        /// <summary>
        /// Sorts the source of items and groups the result by level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="getKey"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<ICollection<T>> Group<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<TKey>> getDependencies, Func<T, TKey> getKey, bool ignoreCycles = true)
        {
            var source2 = source as ICollection<T> ?? source.ToArray();
            return Group<T>(source2, RemapDependencies(source2, getDependencies, getKey), null, ignoreCycles);
        }

        /// <summary>
        /// Sorts the source of items and groups the result by level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="getKey"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<ICollection<T>> Group<T, TKey>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, Func<T, TKey> getKey, bool ignoreCycles = true)
        {
            return Group<T>(source, getDependencies, new GenericEqualityComparer<T, TKey>(getKey), ignoreCycles);
        }

        /// <summary>
        /// Sorts the source of items and groups the result by level.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDependencies"></param>
        /// <param name="comparer"></param>
        /// <param name="ignoreCycles"></param>
        /// <returns></returns>
        public static IList<ICollection<T>> Group<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null, bool ignoreCycles = true)
        {
            var sorted = new List<ICollection<T>>();
            var visited = new Dictionary<T, int>(comparer);

            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited, ignoreCycles);
            }

            return sorted;
        }

        private static int Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<ICollection<T>> sorted, Dictionary<T, int> visited, bool ignoreCycles)
        {
            const int inProcess = -1;
            var alreadyVisited = visited.TryGetValue(item, out var level);

            if (alreadyVisited)
            {
                if (level == inProcess && ignoreCycles)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = level = inProcess;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        var depLevel = Visit(dependency, getDependencies, sorted, visited, ignoreCycles);
                        level = Math.Max(level, depLevel);
                    }
                }

                visited[item] = ++level;
                while (sorted.Count <= level)
                {
                    sorted.Add(new Collection<T>());
                }
                sorted[level].Add(item);
            }
            return level;
        }
    }
}