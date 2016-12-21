using Tempest.Core.Operations.Providers;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class SourcingResult
    {
        public AbstractStreamProvider Provider { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}