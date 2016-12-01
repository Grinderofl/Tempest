using System.IO;

namespace Tempest
{
    public interface IDirectoryFinder
    {
        DirectoryInfo FindGeneratorLibraryDirectory();
    }
}