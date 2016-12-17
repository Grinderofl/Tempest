using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public class OpenFileStreamSource : AbstractStreamSource
    {
        private readonly string _filePath;

        public OpenFileStreamSource(string filePath)
        {
            _filePath = filePath;
        }

        public override Stream Create()
        {
            return File.Open(_filePath, FileMode.Open);
        }

        protected override string GetStreamDescriptor()
        {
            return _filePath;
        }
    }
}