using System;
using System.Collections.Generic;
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
                const string sameKeyPrefix = "An item with the same key has already been added. Key: ";
                try
                {
                    var sortedAspects = TopologicalSort.Sort(_aspects, x => x.Dependencies, x => x.Id, ignoreCycles:false);
                    _aspects.Clear();
                    _aspects.AddRange(sortedAspects);
                }
                catch (ArgumentException e)
                {
                    if (e.Message.StartsWith(sameKeyPrefix))
                    {
                        throw new Exception($"Aspect Id must be unique - {e.Message.Substring(sameKeyPrefix.Length)}");
                    }

                    throw;
                }
                catch (KeyNotFoundException e)
                {
                    var parts = e.Message.Split('\'');
                    //TODO: USe RegEx
                    if (parts.Length == 3)
                    {
                        throw new Exception($"Missing dependency {parts[1]}");
                    }
                    throw;
                }
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