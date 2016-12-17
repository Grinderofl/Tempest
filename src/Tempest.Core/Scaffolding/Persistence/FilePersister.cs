using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public class FilePersister : AbstractStreamPersister
    {
        protected readonly string FilePath;

        public FilePersister(string filePath)
        {
            FilePath = filePath;
        }

        public override void Persist(Stream sourceStream)
        {
            using (var fs = File.Create(FilePath))
            {
                sourceStream.Seek(0, SeekOrigin.Begin);
                sourceStream.CopyTo(fs);
            }
        }

        protected override string GetStreamDescriptor()
        {
            return $"{FilePath}";
        }
    }

}