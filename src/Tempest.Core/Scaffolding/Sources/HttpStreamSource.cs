using System;
using System.IO;
using System.Net.Http;

namespace Tempest.Core.Scaffolding.Sources
{
    public class HttpStreamSource : AbstractStreamSource
    {
        private readonly Uri _uri;
        protected static readonly HttpClient HttpClient = new HttpClient();
        public HttpStreamSource(Uri uri)
        {
            _uri = uri;
        }

        public override Stream Create()
        {
            return HttpClient.GetStreamAsync(_uri).Result;
        }

        protected override string GetStreamDescriptor()
        {
            return _uri.AbsoluteUri;
        }
    }
}