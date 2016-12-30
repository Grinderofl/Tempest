using System;
using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Operations.Transforms
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
            if (stream == null) throw new ArgumentNullException(nameof(stream), $"Could not replace token '{_searchForToken}'");
            return stream.ReadAsString().Replace(_searchForToken, _replaceWith).ToStream();
        }

        protected override string GetTransformerDescription()
        {
            return $"Replace '{_searchForToken}' with '{_replaceWith}'";
        }
    }
}