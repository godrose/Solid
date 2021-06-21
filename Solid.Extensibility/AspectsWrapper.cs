using System.Collections.Generic;
using Solid.Core;

namespace Solid.Extensibility
{
    /// <summary>
    /// Wraps collection of aspects
    /// </summary>
    public class AspectsWrapper :
        IInitializable,
        IHaveAspects<AspectsWrapper>,
        IAspectsProvider
    {
        private readonly List<IAspect> _coreAspects = new List<IAspect>();
        private readonly List<IAspect> _aspects = new List<IAspect>();

        /// <inheritdoc />
        public AspectsWrapper UseAspect(IAspect aspect)
        {
            _aspects.Add(aspect);
            return this;
        }

        /// <inheritdoc cref="IAspectsProvider"/>
        public IEnumerable<IAspect> Aspects => _aspects.ToArray();

        /// <summary>
        /// Use this to inject core aspects.
        /// </summary>
        /// <param name="coreAspects">The collection of core aspects.</param>
        public void UseCoreAspects(IEnumerable<IAspect> coreAspects)
        {
            _coreAspects.AddRange(coreAspects);
        }

        /// <inheritdoc />
        public void Initialize()
        {                        
            _aspects.AddRange(_coreAspects);
            var sortedAspects = _aspects.SortTopologically();
            _aspects.Clear();
            _aspects.AddRange(sortedAspects);
            foreach (var aspect in _aspects)
            {
                aspect.Initialize();
            }
        }
    }
}