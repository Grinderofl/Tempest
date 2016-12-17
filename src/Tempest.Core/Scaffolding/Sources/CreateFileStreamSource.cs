using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public class CreateFileStreamSource : AbstractStreamSource
    {
        private readonly string _filePath;

        public CreateFileStreamSource(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Create()
        {
            return File.Create(_filePath);
        }
    }
}