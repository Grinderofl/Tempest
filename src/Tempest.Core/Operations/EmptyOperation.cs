using Tempest.Core.Operations.Persistence;
using Tempest.Core.Operations.Providers;
using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Operations
{
    public class EmptyOperation : Operation
    {
        public EmptyOperation() : base(EmptyStreamProvider.Instance, EmptyStreamTransformer.Instance, VoidPersister.Instance)
        {
        }
    }
}