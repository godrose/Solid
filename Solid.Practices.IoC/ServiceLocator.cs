using System;

namespace Solid.Practices.IoC
{
    public static class ServiceLocator
    {
        private static IServiceLocator _current;
        public static IServiceLocator Current
        {
            get
            {
                if (_current == null)
                {
                    throw new NullReferenceException("Service Locator must be set");
                }
                return _current;
            }
            set
            {
                _current = value;
            }
        }
    }
}
