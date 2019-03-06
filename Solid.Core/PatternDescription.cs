namespace Solid.Core
{
    public struct PatternDescription
    {
        public PatternDescription(string prefix, string contents, string postfix)
        {
            Prefix = prefix;
            Contents = contents;
            Postfix = postfix;
        }

        public string Prefix { get; }
        public string Contents { get; }
        public string Postfix { get; }
    }
}