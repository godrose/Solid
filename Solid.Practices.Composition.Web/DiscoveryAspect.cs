using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.Web
{
    /// <summary>
    /// The discovery aspect
    /// </summary>
    public sealed class DiscoveryAspect : IAspect, IAssemblySourceProvider
    {
        private readonly CompositionOptions _compositionOptions;

        /// <summary>
        /// Creates an instance of <see cref="DiscoveryAspect"/>
        /// </summary>
        /// <param name="compositionOptions"></param>
        public DiscoveryAspect(CompositionOptions compositionOptions)
        {
            _compositionOptions = compositionOptions;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            GetAssemblies();
        }

        /// <inheritdoc />
        public string Id => "Discovery";

        /// <inheritdoc />
        public string[] Dependencies => new[] { "Modularity", "Platform" };

        private Assembly[] _assemblies;
        /// <inheritdoc />       
        public IEnumerable<Assembly> Assemblies => _assemblies ?? (_assemblies = CreateAssemblies());

        private Assembly[] CreateAssemblies()
        {
            return GetAssemblies();
        }

        private Assembly[] GetAssemblies()
        {
            var rootPath = PlatformProvider.Current.GetAbsolutePath(_compositionOptions.ModulesPath);
            var assembliesResolver = new AssembliesResolver(
                new ServerAssemblySourceProvider(rootPath));
            return ((IAssembliesReadOnlyResolver)assembliesResolver).GetAssemblies().ToArray();
        }
    }
}