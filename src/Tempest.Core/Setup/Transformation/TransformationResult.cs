using System.IO;

namespace Tempest.Core.Setup.Transformation
{
    public class TransformationResult
    {
        public TransformationResult(Stream transformationStream, string filename)
        {
            OutputStream = transformationStream;
            Filename = filename;
        }

        public TransformationResult(Stream stream)
        {
            OutputStream = stream;
        }

        public Stream OutputStream { get; set; }
        public string Filename { get; set; }
        public string FilePath { get; set; }
    }
}