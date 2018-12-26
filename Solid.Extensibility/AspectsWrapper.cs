using System.Collections.Generic;
using System.Linq;
using Solid.Core;

namespace Solid.Extensibility
{
    /// <summary>
    /// Wraps collection of aspects
    /// </summary>
    public class AspectsWrapper :
        IInitializable,
        IHaveAspects<AspectsWrapper>
    {
        private readonly List<IAspect> _coreAspects = new List<IAspect>();
        private readonly List<IAspect> _aspects = new List<IAspect>();

        /// <inheritdoc />
        public AspectsWrapper UseAspect(IAspect aspect)
        {
            _aspects.Add(aspect);
            return this;
        }

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
            void SortAspects()
            {
                //TODO: should use cycle detection and bfs - later
                var sortedAspects = new List<IAspect>();
                var additionalAspects =
                    _aspects.Where(t => t.Id != "Platform" && t.Id != "Modularity" && t.Id != "Discovery").ToArray();
                var platformAspect = _aspects.FirstOrDefault(t => t.Id == "Platform");
                if (platformAspect != null)
                {
                    sortedAspects.Add(platformAspect);
                }
                var modularityAspect = _aspects.FirstOrDefault(t => t.Id == "Modularity");
                if (modularityAspect != null)
                {
                    sortedAspects.Add(modularityAspect);
                }
                var discoveryAspect = _aspects.FirstOrDefault(t => t.Id == "Discovery");
                if (platformAspect != null)
                {
                    sortedAspects.Add(discoveryAspect);
                }
                sortedAspects.AddRange(additionalAspects);
                _aspects.Clear();
                _aspects.AddRange(sortedAspects);
            }

            _aspects.AddRange(_coreAspects);
            SortAspects();
            foreach (var aspect in _aspects)
            {
                aspect.Initialize();
            }
        }
    }
}