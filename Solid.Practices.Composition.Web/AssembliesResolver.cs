using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// Assemblies resolver for server part of web applications
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase, IAssembliesResolver
    {
        public AssembliesResolver(ICompositionModulesProvider compositionModulesProvider) : base(compositionModulesProvider)
        {
        }

        protected override IEnumerable<Assembly> GetRootAssemblies()
        {
            return new []
            {
                GetEntryAssembly()
            };
        }

        ICollection<Assembly> IAssembliesResolver.GetAssemblies()
        {
            return GetAssemblies().ToList();
        }

        static private Assembly GetEntryAssembly()
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
