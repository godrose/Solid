using System;

namespace Solid.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DependenciesAttribute : Attribute
    {      
        public DependenciesAttribute(string[] dependencies = null)
        {
            Dependencies = dependencies ?? (new string[] { });            
        }
        public string[] Dependencies { get; }
    }
}
