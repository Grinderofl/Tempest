using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Transformation
{
    public class TokenTransformer : Transformer
    {
        private readonly string _token;
        private readonly string _replaceWith;

        public TokenTransformer(string token, string replaceWith)
        {
            _token = token;
            _replaceWith = replaceWith;
        }

        protected override Stream TransformCore(TransformerContext context)
        {
            var asString = context.TransformationStream.ReadAsString();
            asString = asString.Replace(_token, _replaceWith);
            return asString.ToStream();
        }
    }
}