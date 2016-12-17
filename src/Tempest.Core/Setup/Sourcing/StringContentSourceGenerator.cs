using System.Collections.Generic;
using Tempest.Core.Scaffolding.Sources;

namespace Tempest.Core.Setup.Sourcing
{
    public class StringContentSourceGenerator : SourceGenerator
    {
        private readonly string _string;

        public StringContentSourceGenerator(string s)
        {
            _string = s;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult
            {
                FileName = "",
                Source = new StringStreamSource(_string),
            };
        }
    }
}