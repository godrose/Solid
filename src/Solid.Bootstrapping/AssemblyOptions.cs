namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents different assembly inclusion/exclustion options.
    /// </summary>
    public class AssemblyOptions
    {
        /// <summary>
        /// Indicates whether all assemblies are included. The default value is true.
        /// </summary>
        public bool IncludeAll { get; set; } = true;

        /// <summary>
        /// Gets or sets the list of prefixes for assemblies to be included. The default value is empty collection.
        /// </summary>
        public string[] IncludedPrefixes { get; set; } = { };

        /// <summary>
        /// Gets or sets the list of prefixes for assemblies to be excluded. The default value is empty collection.
        /// </summary>
        public string[] ExcludedPrefixes { get; set; } = { };
    }
}