using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.Client
{
    /// <summary>
    /// The assemblies discovery aspect. See <see cref="IAspect"/>
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
        public IEnumerable<Assembly> Assemblies => _assemblies ?? (_assemblies = GetAssemblies());

        private Assembly[] GetAssemblies()
        {
            var assembliesResolver = new AssembliesResolver(GetType(),
                new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(),
                    _compositionOptions.Prefixes));
            return ((IAssembliesReadOnlyResolver)assembliesResolver).GetAssemblies().ToArray();
        }
    }
}