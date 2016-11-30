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

        protected override Stream GenerateCore(SourcingContext context)
        {
            return _string.ToStream();
        }
    }

    public class Generators
    {
        public static Source FromString(string source) => new StringContentSource(source);
    }
}