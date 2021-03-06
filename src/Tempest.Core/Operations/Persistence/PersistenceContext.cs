using System.IO;

namespace Tempest.Core.Operations.Persistence
{
    public class PersistenceContext
    {
        public DirectoryInfo TargetDirectory { get; set; }
        public string Filename { get; set; }
        public string FilePath { get; set; }
    }
}