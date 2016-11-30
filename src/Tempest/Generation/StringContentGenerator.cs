using System.IO;
using Tempest.Utils;

namespace Tempest.Generation
{
    public class StringContentGenerator : Generator
    {

        private readonly string _string;

        public StringContentGenerator(string s)
        {
            _string = s;
        }

        protected override Stream GenerateCore(GenesisContext context)
        {
            return _string.ToStream();
        }
    }
}