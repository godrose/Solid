using System;

namespace Solid.IoC.Registration
{
    /// <summary>
    /// Represents an exception that is thrown when no default registration method is found.
    /// </summary>
    public sealed class MissingDefaultRegistrationMethodException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="MissingDefaultRegistrationMethodException"/>
        /// </summary>
        /// <param name="dependencyRegistratorType">The dependency registrator type.</param>
        public MissingDefaultRegistrationMethodException(Type dependencyRegistratorType)
            :base($"Missing default registration method for {dependencyRegistratorType.Name}")
        {
            
        }
    }
}
