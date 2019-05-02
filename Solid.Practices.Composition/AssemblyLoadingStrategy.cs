using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Core;
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
        public PreloadedAssemblyLoadingStrategy(
            IEnumerable<Assembly> assemblies) => Assemblies = assemblies.ToArray();

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
        private readonly string[] _namespaces;
        private readonly string[] _extensions;        

        /// <summary>
        /// Initializes a new instance of the<see cref="FileSystemBasedAssemblyLoadingStrategy"/> class.     
        /// </summary>
        /// <param name="rootPath">Root path for inspection</param>
        /// <param name="prefixes">Allowed prefixes; leave empty if all are allowed.</param>
        /// <param name="namespaces">Allowed namespaces; leave empty if all are allowed.</param>
        /// <param name="extensions">Allowed extensions; leave empty if all are allowed.</param>
        public FileSystemBasedAssemblyLoadingStrategy(
            string rootPath,            
            string[] prefixes = null,
            string[] namespaces = null,
            string[] extensions = null)
        {
            _rootPath = rootPath;
            _prefixes = prefixes;
            _namespaces = namespaces;
            _extensions = extensions;
        }

        /// <inheritdoc />
        public IEnumerable<Assembly> Load() => AssemblyLoader.LoadAssembliesFromPaths(DiscoverFilePaths());
       
        private IEnumerable<string> DiscoverFilePaths()
        {
            var patternsCalculator = new PatternsCalculator();
            var patterns = patternsCalculator.Calculate(_prefixes, _namespaces, _extensions);
            var allFiles = PlatformProvider.Current.GetFiles(_rootPath).Select(Path.GetFileName).ToArray();
            var filePaths = patterns
                .Select(k =>
                    //TODO: Consider RegEx
                    allFiles.Where(t =>
                        (k.Prefix == Consts.WildCard || t.StartsWith(k.Prefix)) && 
                        (k.Contents == Consts.WildCard || t.Contains(k.Contents)) && 
                        (k.Postfix == Consts.WildCard || t.EndsWith(k.Postfix))))
                .SelectMany(k => k);
            return filePaths;
        }
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
        internal static IEnumerable<Assembly> FilterByPrefixes(this IEnumerable<Assembly> assemblies, string[] prefixes) => prefixes?.Length == 0
            ? assemblies
            : assemblies.Where(t => prefixes.Any(k => t.GetName().Name.StartsWith(k)));
    }
}
