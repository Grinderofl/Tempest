using System.IO;

namespace Tempest.Core.Domain.Concrete
{
    public class FilePath
    {
        public string Filename { get; }
        public string Directory { get; }

        public string GetFullPath() => Path.Combine(Directory, Filename);
    }
}