using System;
using System.IO;

namespace Tempest.Core.Transformation
{
    public class TransformerContext
    {
        public Stream TransformationStream { get; set; }

        public string ReadInputAsString()
        {
            throw new NotImplementedException();
        }
    }
}