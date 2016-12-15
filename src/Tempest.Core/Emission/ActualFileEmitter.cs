using System.IO;

namespace Tempest.Core.Emission
{
    public class ActualFileEmitter : ActualEmitter
    {
        private readonly string _filePath;

        public ActualFileEmitter(string filePath)
        {
            _filePath = filePath;
        }

        public override void Emit(Stream sourceStream)
        {
            using (var fs = File.Create(_filePath))
            {
                sourceStream.Seek(0, SeekOrigin.Begin);
                sourceStream.CopyTo(fs);
            }
        }
    }
}