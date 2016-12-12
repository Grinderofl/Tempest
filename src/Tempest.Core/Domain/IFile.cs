using System.IO;

namespace Tempest.Core.Domain
{
    public interface IFile
    {
        string Filename { get; }
        string Directory { get; }
        string FilePath { get; }
        Stream Contents { get; }

        bool Exists();
        void UpdateFilename(string newName);
    }
}
