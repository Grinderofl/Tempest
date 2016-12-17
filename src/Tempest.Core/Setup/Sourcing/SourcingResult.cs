using Tempest.Core.Scaffolding.Providers;

namespace Tempest.Core.Setup.Sourcing
{
    public class SourcingResult
    {
        public AbstractStreamProvider Provider { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}