using System.IO;

namespace Tempest.Core.Domain.Streaming
{
    public class CreateFileStreamFactory : StreamFactory
    {
        private readonly string _filePath;

        public CreateFileStreamFactory(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Create()
        {
            return File.Create(_filePath);
        }
    }
}