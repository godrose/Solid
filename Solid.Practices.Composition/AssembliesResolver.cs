using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The assemblies resolver.
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase
    {
        private readonly Type _entryType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolver"/> class.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="assemblySourceProvider">The assembly source provider.</param>
        public AssembliesResolver(
            IAssemblySourceProvider assemblySourceProvider,
            Type entryType = null) : base(assemblySourceProvider) => _entryType = entryType;

        /// <inheritdoc />       
        protected override IEnumerable<Assembly> GetRootAssemblies() => _entryType == null
            ? new Assembly[] { }
            : Enumerable.Repeat(
                _entryType.GetTypeInfo()
                    .Assembly, 1);
    }
}