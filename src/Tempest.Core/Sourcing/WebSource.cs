using System;
using System.Collections.Generic;
using System.Net.Http;
using Tempest.Core.Domain.Streaming;

namespace Tempest.Core.Sourcing
{
    public class WebSource : Source

    {
        private readonly Uri _uri;
        
        public WebSource(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            _uri = uri;
        }
        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            //var result = HttpClient.GetStreamAsync(_uri).Result;
            yield return new SourcingResult()
            {
                OutputStreamFactory = new HttpStreamFactory(_uri)
                //OutputStream = result
            };
        }
    }
}