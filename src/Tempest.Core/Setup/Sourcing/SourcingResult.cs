using Tempest.Core.Scaffolding.Sources;

namespace Tempest.Core.Setup.Sourcing
{
    public class SourcingResult
    {
        public AbstractStreamSource Source { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}