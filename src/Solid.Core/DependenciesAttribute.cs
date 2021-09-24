using System;

namespace Solid.Core
{
    /// <summary>
    /// The dependencies attribute. Use it to describe the list of dependencies for the current type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DependenciesAttribute : Attribute
    {      
        /// <summary>
        /// Creates an instance of <see cref="DependenciesAttribute"/>
        /// </summary>
        /// <param name="dependencies"></param>
        public DependenciesAttribute(string[] dependencies = null)
        {
            Dependencies = dependencies ?? (new string[] { });            
        }

        /// <summary>
        /// The collection of dependencies
        /// </summary>
        public string[] Dependencies { get; }
    }
}
