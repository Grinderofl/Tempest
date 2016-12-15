using System;
using System.IO;
using System.Net.Http;
using Tempest.Core.Utils;

namespace Tempest.Core.Domain.Streaming
{
    public class StringStreamFactory : StreamFactory
    {
        private readonly string _s;

        public StringStreamFactory(string s)
        {
            _s = s;
        }

        public override Stream Create()
        {
            return _s.ToStream();
        }
    }

    public class HttpStreamFactory : StreamFactory
    {
        private Uri _uri;
        protected static readonly HttpClient HttpClient = new HttpClient();
        public HttpStreamFactory(Uri uri)
        {
            _uri = uri;
        }

        public override Stream Create()
        {
            return HttpClient.GetStreamAsync(_uri).Result;
        }
    }
}