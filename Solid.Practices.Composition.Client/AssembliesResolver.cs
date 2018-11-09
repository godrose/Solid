using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition.Contracts;

namespace Solid.Practices.Composition.Client
{
    /// <summary>
    /// Assemblies resolver for client applications.
    /// </summary>
    public class AssembliesResolver : AssembliesResolverBase
    {
        private readonly Type _entryType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssembliesResolver"/> class.
        /// </summary>
        /// <param name="entryType">Type of the entry.</param>
        /// <param name="assemblySourceProvider">The assembly source provider.</param>
        public AssembliesResolver(Type entryType,
            IAssemblySourceProvider assemblySourceProvider) : base(assemblySourceProvider) => _entryType = entryType;

        /// <inheritdoc />       
        protected override IEnumerable<Assembly> GetRootAssemblies() => Enumerable.Repeat(
            _entryType.GetTypeInfo()
                .Assembly, 1);
    }
}
