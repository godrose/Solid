using System.Collections.Generic;

namespace Solid.Core
{
    /// <summary>
    /// Calculates patterns for file paths.
    /// </summary>
    public class PatternsCalculator
    {
        /// <summary>
        /// Calculates patterns for file paths based on the required conditions.
        /// </summary>
        /// <param name="prefixes">The list of allowed prefixes. Leave empty if all are allowed.</param>
        /// <param name="namespaces">The list of allowed namespaces. Leave empty if all are allowed.</param>
        /// <param name="extensions">The list of allowed extensions. Leave empty if all are allowed.</param>
        /// <returns>The list of allowed patterns.</returns>
        public IEnumerable<PatternDescription> Calculate(string[] prefixes, string[] namespaces, string[] extensions)
        {
            prefixes = prefixes.Patch();
            namespaces = namespaces.Patch();
            extensions = extensions.Patch();

            foreach (var prefix in prefixes)
            {
                foreach (var ns in namespaces)
                {
                    foreach (var extension in extensions)
                    {
                        yield return new PatternDescription(prefix, ns, extension);
                    }
                }
            }
        }
    }
}