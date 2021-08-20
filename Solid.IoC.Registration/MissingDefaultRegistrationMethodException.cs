using System;

namespace Solid.IoC.Registration
{
    public sealed class MissingDefaultRegistrationMethodException : Exception
    {
        public MissingDefaultRegistrationMethodException(Type dependencyRegistratorType)
            :base($"Missing default registration method for {dependencyRegistratorType.Name}")
        {
            
        }
    }
}
