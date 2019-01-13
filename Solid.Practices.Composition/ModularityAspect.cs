using System;
using System.Collections.Generic;
using System.Linq;
using Solid.Core;
using Solid.Extensibility;
using Solid.Practices.Composition.Container;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition
{
    /// <summary>
    /// The modularity aspect. See <see cref="IAspect"/>
    /// </summary>
    public sealed class ModularityAspect :
        IAspect,
        ICompositionModulesProvider,
        IHaveErrors
    {
        private readonly CompositionOptions _options;

        /// <summary>
        /// Creates an instance of <see cref="ModularityAspect"/> with empty <see cref="CompositionOptions"/>
        /// </summary>
        public ModularityAspect() :
            this(new CompositionOptions())
        {

        }

        /// <summary>
        /// Creates an instance of <see cref="ModularityAspect"/> using provided <see cref="CompositionOptions"/>
        /// </summary>
        /// <param name="options">The composition options</param>
        public ModularityAspect(CompositionOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public IEnumerable<ICompositionModule> Modules { get; private set; } = new ICompositionModule[] { };

        /// <inheritdoc />
        public IEnumerable<Exception> Errors { get; private set; } = new Exception[] { };

        /// <summary>
        /// The relative path.
        /// </summary>
        public string RelativePath => _options.RelativePath;

        /// <summary>
        /// The modules' prefixes.
        /// </summary>
        public string[] Prefixes => _options.Prefixes;

        /// <inheritdoc />
        public void Initialize()
        {
            CompositionManager compositionManager = new CompositionManager();
            ModularityInfo modularityInfo = new ModularityInfo();
            try
            {
                compositionManager.Initialize(RelativePath, Prefixes);
            }
            catch (AggregateAssemblyInspectionException ex)
            {
                modularityInfo.Errors = ex.InnerExceptions;
            }
            finally
            {
                modularityInfo.Modules = compositionManager.Modules == null
                    ? new ICompositionModule[0]
                    : compositionManager.Modules.ToArray();
            }

            Modules = modularityInfo.Modules;
            Errors = modularityInfo.Errors;
        }

        /// <inheritdoc />
        public string Id => "Modularity";

        //TODO: Add explicit dep on Discovery when assemblies loading strategies are merged
        /// <inheritdoc />
        public string[] Dependencies => new[] { "Platform"};
    }
}