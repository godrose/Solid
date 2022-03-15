using System;

namespace Solid.Core
{
    /// <summary>
    /// The identity attribute. Use it to assign a unique identifiable value to the type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]    
    public sealed class IdAttribute : Attribute
    {
        /// <summary>
        /// Creates an instance of <see cref="IdAttribute"/>
        /// </summary>
        /// <param name="id"></param>
        public IdAttribute(string id)
        {
            Id = id;
        }

        /// <summary>
        /// The id.
        /// </summary>
        public string Id { get; }
    }
}
