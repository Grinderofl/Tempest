using System.Collections.Generic;
using Tempest.Core.Operations.Providers;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class StringContentSourceFactory : SourceFactory
    {
        private readonly string _string;

        public StringContentSourceFactory(string s)
        {
            _string = s;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult
            {
                FileName = "",
                Provider = new StringStreamProvider(_string),
            };
        }
    }
}