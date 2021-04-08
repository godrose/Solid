using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UpdateUtil
{
    internal static class NavigationHelper
    {
        internal static void NavigateToBin()
        {
            Cd("update");
            Cd("UpdateUtil");
            Cd("bin");
        }

        internal static void GoUp(int numberOfLevels)
        {
            var relativePath = new List<string> { Directory.GetCurrentDirectory() };
            relativePath.AddRange(Enumerable.Repeat("..", numberOfLevels));
            Directory.SetCurrentDirectory(Path.Combine(relativePath.ToArray()));
        }

        internal static void Cd(string directory)
        {
            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), directory));
        }
    }
}
