using System;

namespace Solid.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class IdAttribute : Attribute
    {
        public IdAttribute(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }
}
