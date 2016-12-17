using System.IO;
using Tempest.Core.Scaffolding.Transforms;
using Tempest.Core.Utils;

namespace Tempest.Core.Setup.Transformation
{
    public class TokenOperationTransformer : OperationTransformer
    {
        private readonly bool _replaceFileNames;
        private readonly string _replaceWith;
        private readonly string _token;

        public TokenOperationTransformer(string token, string replaceWith, bool replaceFileNames = true)
        {
            _token = token;
            _replaceWith = replaceWith;
            _replaceFileNames = replaceFileNames;
        }

        public override IStreamTransformer CreateStreamTransformer() => new TokenStreamTransformer(_token, _replaceWith);

        public override string TransformFilename(string source)
            => _replaceFileNames ? source?.Replace(_token, _replaceWith) : source;
    }
}