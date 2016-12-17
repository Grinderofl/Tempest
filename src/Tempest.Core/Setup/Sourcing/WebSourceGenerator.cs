using System;
using System.Collections.Generic;
using Tempest.Core.Scaffolding.Sources;

namespace Tempest.Core.Setup.Sourcing
{
    public class WebSourceGenerator : SourceGenerator
    {
        private readonly Uri _uri;
        
        public WebSourceGenerator(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            _uri = uri;
        }
        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult()
            {
                Source = new HttpStreamSource(_uri)
            };
        }
    }
}