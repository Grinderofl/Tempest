using Tempest.Core.Domain;

namespace Tempest.Core.Persistence
{
    public interface IFileSystem
    {
        IFile Open(string path);
        void Write(IFile file);
    }
}