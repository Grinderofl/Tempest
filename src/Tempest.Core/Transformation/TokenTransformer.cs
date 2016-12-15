using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Transformation
{
    public class TokenTransformer : Transformer
    {
        private readonly bool _replaceFileNames;
        private readonly string _replaceWith;
        private readonly string _token;

        public TokenTransformer(string token, string replaceWith, bool replaceFileNames = true)
        {
            _token = token;
            _replaceWith = replaceWith;
            _replaceFileNames = replaceFileNames;
        }

        public override Stream TransformStream(Stream context)
        {
            var asString = context.ReadAsString();
            asString = asString.Replace(_token, _replaceWith);
            return asString.ToStream();
        }

        public override string TransformFilename(string source)
            => _replaceFileNames ? source.Replace(_token, _replaceWith) : source;
    }
}