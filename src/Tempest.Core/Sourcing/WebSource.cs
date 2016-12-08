using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Tempest.Core.Sourcing
{
    public class WebSource : Source

    {
        private readonly Uri _uri;
        protected static readonly HttpClient HttpClient = new HttpClient();
        public WebSource(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            _uri = uri;
        }
        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var result = HttpClient.GetStreamAsync(_uri).Result;
            yield return new SourcingResult()
            {
                OutputStream = result
            };
        }
    }
}