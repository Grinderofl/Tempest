using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Configuration.Operations.Transformation
{
    public class EmptyOperationTransformer : OperationTransformerBase
    {
        public override IStreamTransformer CreateStreamTransformer()
        {
            return EmptyStreamTransformer.Instance;
        }

    }
}