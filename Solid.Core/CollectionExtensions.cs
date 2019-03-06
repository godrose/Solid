namespace Solid.Core
{
    internal static class CollectionExtensions
    {
        private const string WildCard = "*";

        internal static string[] Patch(this string[] input)
        {
            return input == null || input.Length == 0 ? new[] { WildCard } : input;
        }
    }
}