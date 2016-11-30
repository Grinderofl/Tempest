using System;
using System.IO;

namespace Tempest.Transformation
{
    public class TransformerContext
    {
        public Stream InputStream { get; set; }

        public string ReadInputAsString()
        {
            throw new NotImplementedException();
        }
    }
}