using System.Collections.Generic;

namespace Solid.Core
{
    public class PatternsCalculator
    {
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