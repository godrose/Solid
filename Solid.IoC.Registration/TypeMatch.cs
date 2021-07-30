using System;

namespace Solid.IoC.Registration
{
    public class TypeMatch
    {
        public TypeMatch(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }

        public Type ServiceType { get; }
        public Type ImplementationType { get; }
    }
}