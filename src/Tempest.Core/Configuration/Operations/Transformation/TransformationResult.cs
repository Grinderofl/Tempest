using System.IO;

namespace Tempest.Core.Configuration.Operations.Transformation
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
    }
}