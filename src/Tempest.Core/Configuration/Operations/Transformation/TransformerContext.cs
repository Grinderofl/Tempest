using System.IO;

namespace Tempest.Core.Configuration.Operations.Transformation
{
    public class TransformerContext
    {
        public TransformerContext(string sourceFileName, Stream sourceOutputStream)
        {
            Filename = sourceFileName;
            TransformationStream = sourceOutputStream;
        }

        public Stream TransformationStream { get; set; }
        public string Filename { get; set; }
    }
}