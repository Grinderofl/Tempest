using System.Collections.Generic;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public abstract class SourceFactory
    {
        public abstract IEnumerable<SourcingResult> Generate(SourcingContext context);
    }
}