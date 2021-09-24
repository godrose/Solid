using System;

namespace Solid.IoC.Registration
{
    /// <summary>
    /// Represents match between two types; used during registration phase.
    /// </summary>
    public class TypeMatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMatch"/> class.
        /// </summary>
        /// <param name="serviceType">The required service type.</param>
        /// <param name="implementationType">The matching implementation type.</param>
        public TypeMatch(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }

        /// <summary>
        /// Gets the required service type.
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Gets the matching implementation type.
        /// </summary>
        public Type ImplementationType { get; }
    }
}