namespace Solid.Core
{
    internal static class CollectionExtensions
    {         
        internal static string[] Patch(this string[] input)
        {
            return input == null || input.Length == 0 ? new[] { Consts.WildCard } : input;
        }
    }
}