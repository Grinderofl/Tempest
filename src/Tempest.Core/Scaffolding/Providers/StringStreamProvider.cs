using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Scaffolding.Providers
{
    public class StringStreamProvider : AbstractStreamProvider
    {
        private readonly string _s;

        public StringStreamProvider(string s)
        {
            _s = s;
        }

        public override Stream Provide()
        {
            return _s.ToStream();
        }

        protected override string GetStreamDescriptor()
        {
            return _s;
        }
    }
}