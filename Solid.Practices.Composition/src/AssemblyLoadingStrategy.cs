using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// Represents a pre-loaded assemblies' based assembly loading strategy.
    /// </summary>
    public class PreloadedAssemblyLoadingStrategy : IAssemblyLoadingStrategy
    {
        private IEnumerable<Assembly> Assemblies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreloadedAssemblyLoadingStrategy"/> class.
        /// </summary>
        /// <param name="assemblies">The preloaded assemblies.</param>
        /// <param name="prefixes">Allowed prefixes; leave empty if all are allowed.</param>
        public PreloadedAssemblyLoadingStrategy(
            IEnumerable<Assembly> assemblies,
            string[] prefixes = null)
        {
            Assemblies = assemblies.FilterByPrefixes(prefixes).ToArray();
        }

        /// <inheritdoc />
        public IEnumerable<Assembly> Load() => Assemblies;
    }

    /// <summary>
    /// Represents a file-system based assembly loading strategy.
    /// </summary>
    public class FileSystemBasedAssemblyLoadingStrategy : IAssemblyLoadingStrategy
    {
        private readonly string _rootPath;
        private readonly string[] _prefixes;
        private static readonly string[] AllowedModulePatterns = { "*.dll", "*.exe" };

        /// <summary>
        /// Initializes a new instance of the<see cref="FileSystemBasedAssemblyLoadingStrategy"/> class.     
        /// </summary>
        /// <param name="rootPath">Root path for inspection</param>
        /// <param name="prefixes">Allowed prefixes; leave empty if all are allowed.</param>
        public FileSystemBasedAssemblyLoadingStrategy(
            string rootPath,
            string[] prefixes = null)
        {
            _rootPath = rootPath;
            _prefixes = prefixes;
        }

        /// <inheritdoc />
        public IEnumerable<Assembly> Load() => SafeAssemblyLoader.LoadAssembliesFromNames(DiscoverAssemblyNames());

        private IEnumerable<string> DiscoverAssemblyNames() => DiscoverFilePaths().Select(Path.GetFileNameWithoutExtension);

        private IEnumerable<string> DiscoverFilePaths() => AllowedModulePatterns.Select(searchPattern =>
        {
            return _prefixes == null || _prefixes.Length == 0
                ? PlatformProvider.Current.GetFiles(_rootPath, searchPattern)
                : _prefixes.Select(prefix => PlatformProvider.Current.GetFiles(_rootPath, prefix + searchPattern))
                    .SelectMany(t => t)
                    .ToArray();
        }).SelectMany(k => k);
    }

    /// <summary>
    /// Represents a type-based assembly loading strategy.
    /// </summary>
    public class TypeBasedAssemblyLoadingStrategy : IAssemblyLoadingStrategy
    {
        private readonly IEnumerable<Type> _types;
        private readonly string[] _prefixes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBasedAssemblyLoadingStrategy"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="prefixes">Allowed prefixes; leave empty if all are allowed.</param>
        public TypeBasedAssemblyLoadingStrategy(
            IEnumerable<Type> types,
            string[] prefixes = null)
        {
            _types = types;
            _prefixes = prefixes;
        }

        /// <inheritdoc/>
        public IEnumerable<Assembly> Load()
        {
            var allAssemblies = _types.Select(t => t.GetTypeInfo().Assembly);
            return allAssemblies.FilterByPrefixes(_prefixes).ToArray();            
        }
    }

    internal static class AssembliesExtensions
    {
        internal static IEnumerable<Assembly> FilterByPrefixes(this IEnumerable<Assembly> assemblies, string[] prefixes)
        {
            return prefixes?.Length == 0
                ? assemblies
                : assemblies.Where(t => prefixes.Any(k => t.GetName().Name.StartsWith(k)));
        }
    }
}
