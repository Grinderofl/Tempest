using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Sourcing
{
    /// <summary>
    /// Generates an output stream
    /// Could be:
    /// GenerateEmpty
    /// GenerateByCopying
    /// GenerateFromWebstream
    /// </summary>
    public abstract class Source
    {
        public abstract IEnumerable<SourcingResult> Generate(SourcingContext context);
    }
}