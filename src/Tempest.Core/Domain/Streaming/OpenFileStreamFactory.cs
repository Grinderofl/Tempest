using System.IO;

namespace Tempest.Core.Domain.Streaming
{
    public class OpenFileStreamFactory : StreamFactory
    {
        private readonly string _filePath;

        public OpenFileStreamFactory(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Create()
        {
            return File.Open(_filePath, FileMode.Open);
        }
    }
}