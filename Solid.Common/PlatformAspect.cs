using Solid.Extensibility;

namespace Solid.Common
{
    /// <summary>
    /// The platform aspect. See <see cref="IAspect"/>
    /// </summary>
    public sealed class PlatformAspect : IAspect
    {
        /// <inheritdoc />
        public void Initialize()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        /// <inheritdoc />
        public string Id => "Platform";

        /// <inheritdoc />
        public string[] Dependencies => new string[] { };
    }
}