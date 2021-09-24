using System;
using System.Collections.Generic;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Enables defining set of conventions used during the assembly loading.
    /// </summary>
    public static class AssemblyLoadingManager
    {
        /// <summary>
        /// Calculates the list of client namespaces to be looked for during assembly discovery.
        /// </summary>
        public static Func<IEnumerable<string>> ClientNamespaces = () => new[] { "Gui", "UI", "Presentation" };

        /// <summary>
        /// Calculates the list of server namespaces to be looked for during assembly discovery.
        /// </summary>
        public static Func<IEnumerable<string>> ServerNamespaces = () => new[] { "Controllers", "Api"};

        /// <summary>
        /// Calculates the list of supported assembly extensions.
        /// </summary>
        public static Func<IEnumerable<string>> Extensions = () => new[] {".dll", ".exe"};
    }
}
