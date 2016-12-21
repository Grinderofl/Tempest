using System.Collections.Generic;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    /// <summary>
    ///     Generates an output stream
    ///     Could be:
    ///     GenerateEmpty
    ///     GenerateByCopying
    ///     GenerateFromWebstream
    /// </summary>
    public abstract class SourceFactory
    {
        public abstract IEnumerable<SourcingResult> Generate(SourcingContext context);
    }
}