using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Transformation
{
    public class TokenTransformer : Transformer
    {
        private readonly string _token;
        private readonly string _replaceWith;
        private readonly bool _replaceFileNames;

        public TokenTransformer(string token, string replaceWith, bool replaceFileNames = true)
        {
            _token = token;
            _replaceWith = replaceWith;
            _replaceFileNames = replaceFileNames;
        }

        protected override Stream TransformStream(TransformerContext context)
        {
            var asString = context.TransformationStream.ReadAsString();
            asString = asString.Replace(_token, _replaceWith);
            return asString.ToStream();
        }

        protected override string TransformFilename(string source)
            => _replaceFileNames ? source.Replace(_token, _replaceWith) : source;
    }
}