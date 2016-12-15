using System.IO;
using Tempest.Core.Domain.Streaming;

namespace Tempest.Core.Sourcing
{
    public class SourcingResult
    {
        //public Stream OutputStream { get; set; }
        public StreamFactory OutputStreamFactory { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}