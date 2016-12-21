using System.IO;
using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Setup.Transformation
{
    public class EmptyOperationTransformer : OperationTransformer
    {
        public override IStreamTransformer CreateStreamTransformer()
        {
            return EmptyStreamTransformer.Instance;
        }

    }
}