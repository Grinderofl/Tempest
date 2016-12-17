using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Scaffolding.Sources
{
    public class StringStreamSource : AbstractStreamSource
    {
        private readonly string _s;

        public StringStreamSource(string s)
        {
            _s = s;
        }

        public override Stream Create()
        {
            return _s.ToStream();
        }

        protected override string GetStreamDescriptor()
        {
            return _s;
        }
    }
}