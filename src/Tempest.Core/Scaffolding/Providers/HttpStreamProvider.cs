using System;
using System.IO;
using System.Net.Http;

namespace Tempest.Core.Scaffolding.Providers
{
    public class HttpStreamProvider : AbstractStreamProvider
    {
        private readonly Uri _uri;
        protected static readonly HttpClient HttpClient = new HttpClient();
        public HttpStreamProvider(Uri uri)
        {
            _uri = uri;
        }

        public override Stream Provide()
        {
            return HttpClient.GetStreamAsync(_uri).Result;
        }

        protected override string GetStreamDescriptor()
        {
            return _uri.AbsoluteUri;
        }
    }
}