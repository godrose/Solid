using System;
using System.Collections.Generic;
using System.Reflection;

namespace Solid.IoC.Registration
{
    internal static class TypeExtensions
    {
        internal static IEnumerable<Type> GetImplementedInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }
    }
}
