namespace Solid.Practices.Composition
{
    /// <summary>
    /// The composition options
    /// </summary>
    public class CompositionOptions
    {
        /// <summary>
        /// The relative path.
        /// </summary>
        public string RelativePath { get; set; } = ".";

        /// <summary>
        /// The modules' prefixes.
        /// </summary>
        public string[] Prefixes { get; set; } = { };
    }
}
