using System.Collections.Generic;
using System.Reflection;

namespace Solid.Practices.Composition
{
    public interface IAssembliesReadOnlyResolver
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}