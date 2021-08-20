using System;
using System.Collections.Generic;

namespace Solid.IoC.Registration
{
    public static class RegistrationMethodContext
    {
        private static readonly Dictionary<Type, Delegate> Storage =
            new Dictionary<Type, Delegate>();

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

        public static void ClearRegistrations()
        {
            Storage.Clear();
        }
    }
}
