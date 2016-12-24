using System.Collections.Generic;
using Tempest.Core.Operations.Providers;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class StringContentSourceFactory : SourceFactory
    {
        private readonly string _string;
        private readonly string _filename;

        public StringContentSourceFactory(string source, string filename = "")
        {
            _string = source;
            _filename = filename;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult
            {
                FileName = _filename,
                Provider = new StringStreamProvider(_string),
            };
        }
    }
}