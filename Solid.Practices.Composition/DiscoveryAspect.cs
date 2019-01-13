using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Extensibility;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The discovery aspect.
    /// </summary>
    public class DiscoveryAspect : IAspect, IAssemblySourceProvider
    {
        private readonly CompositionOptions _compositionOptions;
        private readonly Type _rootType;

        /// <summary>
        /// Creates an instance of <see cref="DiscoveryAspect"/>
        /// </summary>
        /// <param name="compositionOptions"></param>
        /// <param name="rootType"></param>
        public DiscoveryAspect(CompositionOptions compositionOptions, Type rootType = null)
        {
            _compositionOptions = compositionOptions;
            _rootType = rootType;
        }

        /// <inheritdoc />
        public void Initialize()
        {
            _assemblies = GetAssemblies();
        }

        /// <inheritdoc />
        public string Id => "Discovery";

        /// <inheritdoc />
        public string[] Dependencies => new[] { "Platform" };

        private Assembly[] _assemblies;

        /// <inheritdoc />
        public IEnumerable<Assembly> Assemblies => _assemblies ?? (_assemblies = GetAssemblies());

        private Assembly[] GetAssemblies()
        {
            var assembliesResolver = new AssembliesResolver(
                new CustomAssemblySourceProvider(
                    PlatformProvider.Current.GetAbsolutePath(_compositionOptions.RelativePath),
                    _compositionOptions.Prefixes), _rootType);
            return ((IAssembliesReadOnlyResolver) assembliesResolver).GetAssemblies().ToArray();
        }
    }
}