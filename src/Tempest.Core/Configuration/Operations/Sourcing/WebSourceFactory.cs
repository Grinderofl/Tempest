using System;
using System.Collections.Generic;
using Tempest.Core.Operations.Providers;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class WebSourceFactory : SourceFactory
    {
        private readonly Uri _uri;
        
        public WebSourceFactory(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            _uri = uri;
        }
        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            yield return new SourcingResult()
            {
                Provider = new HttpStreamProvider(_uri)
            };
        }
    }
}