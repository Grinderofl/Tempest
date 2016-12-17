using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Providers;
using Tempest.Core.Scaffolding.Transforms;

namespace Tempest.Core.Scaffolding.Operations
{
    public class EmptyOperation : Operation
    {
        public EmptyOperation() : base(EmptyStreamProvider.Instance, EmptyStreamTransformer.Instance, VoidPersister.Instance)
        {
        }
    }
}