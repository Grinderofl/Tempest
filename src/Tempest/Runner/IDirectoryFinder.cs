using System.Collections.Generic;
using System.IO;

namespace Tempest.Runner
{
    public interface IDirectoryFinder
    {
        DirectoryInfo FindTempestExecutableDirectory();
        IEnumerable<DirectoryInfo> FindGeneratorDirectories();
        DirectoryInfo FindWorkingDirectory();
    }
}