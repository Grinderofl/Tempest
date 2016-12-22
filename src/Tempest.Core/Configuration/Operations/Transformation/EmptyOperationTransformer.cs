using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Configuration.Operations.Transformation
{
    public class EmptyOperationTransformer : OperationTransformer
    {
        public override IStreamTransformer CreateStreamTransformer()
        {
            return EmptyStreamTransformer.Instance;
        }

    }
}