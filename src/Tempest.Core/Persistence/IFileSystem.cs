using Tempest.Core.Domain;

namespace Tempest.Core.Persistence
{
    public interface IFileSystem
    {
        /// <summary>
        /// Opens a file. Throws if file does not exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IFile Open(string path);
        void Write(IFile file);
    }
}