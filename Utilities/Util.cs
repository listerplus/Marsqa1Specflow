using System;

namespace Marsqa1Specflow.Utilities
{
    public class Util
    {
        public static string GetProjRootDir()
        {
            string currentDir = Directory.GetCurrentDirectory();
            return currentDir.Split("bin")[0];
        }
    }
}
