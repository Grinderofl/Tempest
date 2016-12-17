using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Scaffolding.Transforms
{
    public class TokenStreamTransformer : AbstractStreamTransformer
    {
        private readonly string _searchForToken;
        private readonly string _replaceWith;

        public TokenStreamTransformer(string searchForToken, string replaceWith)
        {
            _searchForToken = searchForToken;
            _replaceWith = replaceWith;
        }

        public override Stream Transform(Stream stream)
        {
            return stream.ReadAsString().Replace(_searchForToken, _replaceWith).ToStream();
        }
    }
}