using System.IO;

namespace Tempest.Core.Transformation
{
    public class TransformationResult
    {
        public Stream OutputStream { get; set; }
        public string Filename { get; set; }
    }
}