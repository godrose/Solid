namespace Solid.Core
{
    /// <summary>
    /// The file path pattern description.
    /// </summary>
    public struct PatternDescription
    {
        /// <summary>
        /// Initializes an instance of <see cref="PatternDescription"/>
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="postfix">The postfix.</param>
        public PatternDescription(string prefix, string contents, string postfix)
        {
            Prefix = prefix;
            Contents = contents;
            Postfix = postfix;
        }

        /// <summary>
        /// The prefix.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// The contents.
        /// </summary>
        public string Contents { get; }

        /// <summary>
        /// The postfix.
        /// </summary>
        public string Postfix { get; }
    }
}