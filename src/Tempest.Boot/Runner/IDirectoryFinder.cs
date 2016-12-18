using System.Collections.Generic;
using System.IO;

namespace Tempest.Boot.Runner
{
    public interface IDirectoryFinder
    {
        DirectoryInfo FindTempestExecutableDirectory();
        IEnumerable<DirectoryInfo> FindGeneratorDirectories(string additionalSearchPath = null);
        DirectoryInfo FindWorkingDirectory();
    }
}