namespace UpdateUtil
{
    class VersionInfo
    {
        public VersionInfo(string version)
        {
            var versionParts = version.Split(new[] { '-' });
            VersionCore = versionParts[0];
            PreRelease = versionParts.Length > 1 ? versionParts[1] : null;
        }

        public string VersionCore { get;  }
        public string PreRelease { get; }

        public override string ToString()
        {
            return PreRelease == null ? VersionCore : $"{VersionCore}-{PreRelease}";
        }
    }
}