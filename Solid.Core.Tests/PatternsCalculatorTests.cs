using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Solid.Core.Tests
{
    public class PatternsCalculatorTests
    {
        public class InputData
        {
            public InputData(string[] prefixes, string[] namespaces, string[] extensions)
            {
                Prefixes = prefixes;
                Namespaces = namespaces;
                Extensions = extensions;
            }

            public string[] Prefixes { get; }
            public string[] Namespaces { get; }
            public string[] Extensions { get; }
        }

        public static IEnumerable<object[]> NonEmptyCollectionsData =>
            new List<object[]>
            {
                new object[]
                {
                    new InputData(new[] {"Prefix"}, new[] {"Namespace"}, new[] {".ext"}),
                    new[] {new PatternDescription("Prefix", "Namespace", ".ext")}
                },
                new object[]
                {
                    new InputData(new[] {"Prefix"}, new[] {"Namespace1", "Namespace2"}, new[] {".ext"}),
                    new[]
                    {
                        new PatternDescription("Prefix", "Namespace1", ".ext"),
                        new PatternDescription("Prefix", "Namespace2", ".ext")
                    }
                },
                new object[]
                {
                    new InputData(new[] {"Prefix"}, new[] {"Namespace1", "Namespace2"}, new[] {".ext1", ".ext2"}),
                    new[]
                    {
                        new PatternDescription("Prefix", "Namespace1", ".ext1"),
                        new PatternDescription("Prefix", "Namespace1", ".ext2"),
                        new PatternDescription("Prefix", "Namespace2", ".ext1"),
                        new PatternDescription("Prefix", "Namespace2", ".ext2")
                    }
                },
                new object[]
                {
                    new InputData(new[] {"Prefix1", "Prefix2"}, new[] {"Namespace1", "Namespace2"}, new[] {".ext1", ".ext2"}),
                    new[]
                    {
                        new PatternDescription("Prefix1", "Namespace1", ".ext1"),
                        new PatternDescription("Prefix1", "Namespace1", ".ext2"),
                        new PatternDescription("Prefix1", "Namespace2", ".ext1"),
                        new PatternDescription("Prefix1", "Namespace2", ".ext2"),
                        new PatternDescription("Prefix2", "Namespace1", ".ext1"),
                        new PatternDescription("Prefix2", "Namespace1", ".ext2"),
                        new PatternDescription("Prefix2", "Namespace2", ".ext1"),
                        new PatternDescription("Prefix2", "Namespace2", ".ext2")
                    }
                }
            };

        [Theory]
        [MemberData(nameof(NonEmptyCollectionsData))]
        public void Search_AllCollectionsAreNonEmpty_ExplicitPathsAreReturned(InputData input, PatternDescription[] expectedOutput)
        {           
            var searcher = new PatternsCalculator();
            var paths = searcher.Calculate(input.Prefixes, input.Namespaces, input.Extensions).ToArray();

            paths.Should().BeEquivalentTo(expectedOutput);
        }
    }

    public struct PatternDescription
    {
        public PatternDescription(string prefix, string contents, string postfix)
        {
            Prefix = prefix;
            Contents = contents;
            Postfix = postfix;
        }

        public string Prefix { get; private set; }
        public string Contents { get; private set; }
        public string Postfix { get; private set; }
    }

    internal class PatternsCalculator
    {
        public IEnumerable<PatternDescription> Calculate(string[] prefixes, string[] namespaces, string[] extensions)
        {
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