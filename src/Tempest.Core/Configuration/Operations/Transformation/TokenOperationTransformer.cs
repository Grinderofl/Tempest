using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Configuration.Operations.Transformation
{
    public class TokenOperationTransformer : OperationTransformerBase
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