namespace Solid.Practices.Composition
{
    /// <summary>
    /// The composition options
    /// </summary>
    public class CompositionOptions
    {
        /// <summary>
        /// The relative modules' path.
        /// </summary>
        public string ModulesPath { get; set; } = ".";

        /// <summary>
        /// The modules' prefixes.
        /// </summary>
        public string[] Prefixes { get; set; } = { };
    }
}
