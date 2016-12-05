using System.Collections.Generic;
using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Sourcing
{
    public class StringContentSource : Source
    {

        private readonly string _string;

        public StringContentSource(string s)
        {
            _string = s;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult()
            {
                FileName = "",
                OutputStream = _string.ToStream()
            };
        }
    }
}