using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Transformation
{
    public class TransformerContext
    {
        public Stream TransformationStream { get; set; }
        public string Filename { get; set; }

        public string ReadInputAsString()
        {
            return TransformationStream.ReadAsString();
        }
    }
}