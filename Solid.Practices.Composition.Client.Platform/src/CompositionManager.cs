using System;

namespace Solid.Practices.Composition.Desktop
{
    /// <summary>
    /// Composition initialization facade for client part of desktop applications
    /// </summary>
    public class CompositionManager : Composition.CompositionManager
    {
        private readonly Type _entryType;
        private readonly string _rootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionManager"/> class.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="rootPath">The root path.</param>
        public CompositionManager(Type entryType, string rootPath)
        {
            _entryType = entryType;
            _rootPath = rootPath;
        }        
    }
}
