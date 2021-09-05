using System;
using System.Collections.Generic;

namespace Solid.IoC.Registration
{
    /// <summary>
    /// Represents container for default registration methods.
    /// </summary>
    public static class RegistrationMethodContext
    {
        private static readonly Dictionary<Type, Delegate> Storage =
            new Dictionary<Type, Delegate>();

        /// <summary>
        /// Gets default registration method for the provided dependency registrator type.
        /// </summary>
        /// <typeparam name="TDependencyRegistrator">The dependency registrator type.</typeparam>
        /// <returns></returns>
        public static Action<TDependencyRegistrator, TypeMatch> GetDefaultRegistrationMethod<TDependencyRegistrator>()
        {
            var key = typeof(TDependencyRegistrator);
            if (!Storage.ContainsKey(key))
            {
                throw new MissingDefaultRegistrationMethodException(key);
            }
            else
            {
                return Storage[key] as Action<TDependencyRegistrator, TypeMatch>;
            }
        }

        /// <summary>
        /// Sets default registration method for the provided dependency registrator type.
        /// </summary>
        /// <typeparam name="TDependencyRegistrator">The dependency registrator type.</typeparam>
        /// <param name="registrationMethod">The default registration method.</param>
        public static void SetDefaultRegistrationMethod<TDependencyRegistrator>(Action<TDependencyRegistrator, TypeMatch> registrationMethod)
        {
            var key = typeof(TDependencyRegistrator);
            if (!Storage.ContainsKey(key))
            {
                Storage.Add(key, registrationMethod);
            }
            else
            {
                Storage[key] = registrationMethod;
            }
        }

        /// <summary>
        /// Clears all existing default registration methods.
        /// </summary>
        public static void ClearRegistrations()
        {
            Storage.Clear();
        }
    }
}
