using System.IO;

namespace Tempest.Core.Operations.Providers
{
    public class CreateFileStreamProvider : AbstractStreamProvider
    {
        private readonly string _filePath;

        public CreateFileStreamProvider(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Provide()
        {
            return File.Create(_filePath);
        }

        protected override string GetStreamDescriptor()
        {
            return _filePath;
        }
    }
}