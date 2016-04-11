using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Assemblies resolver for server part of web applications.
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase, IAssembliesResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolver"/> class.
        /// </summary>        
        /// <param name="assemblySourceProvider">The assembly source provider.</param>
        public AssembliesResolver(
            IAssemblySourceProvider assemblySourceProvider) : base(assemblySourceProvider)
        {           
        }

        /// <summary>
        /// Override this method to retrieve platform-specific root assemblies.
        /// </summary>
        /// <returns>Collection of assemblies</returns>
        protected override IEnumerable<Assembly> GetRootAssemblies()
        {
            return new []
            {
                GetEntryAssembly()
            };
        }

        ICollection<Assembly> IAssembliesResolver.GetAssemblies()
        {
            return ((IAssembliesReadOnlyResolver) this).GetAssemblies().ToList();
        }

        private static Assembly GetEntryAssembly()
        {
            if (HttpContext.Current == null ||
                HttpContext.Current.ApplicationInstance == null)
            {
                return null;
            }

            var type = HttpContext.Current.ApplicationInstance.GetType();
            while (type != null && type.BaseType != null && type.BaseType.Name != "HttpApplication")
            {
                type = type.BaseType;
            }

            return type == null ? null : type.Assembly;
        }       
    }
}
