using System.Collections.Generic;
using System.IO;

namespace Tempest.Boot.Runner.Activation
{
    public interface IDirectoryFinder
    {
        DirectoryInfo FindTempestExecutableDirectory();
        IEnumerable<DirectoryInfo> FindGeneratorDirectories(string additionalSearchPath = null);
        DirectoryInfo FindWorkingDirectory();
    }
}