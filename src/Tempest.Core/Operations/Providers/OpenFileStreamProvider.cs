using System.IO;

namespace Tempest.Core.Operations.Providers
{
    public class OpenFileStreamProvider : AbstractStreamProvider
    {
        private readonly string _filePath;

        public OpenFileStreamProvider(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Provide()
        {
            return File.Open(_filePath, FileMode.Open);
        }

        protected override string GetStreamDescriptor()
        {
            return _filePath;
        }
    }
}