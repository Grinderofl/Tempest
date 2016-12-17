using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Sources;
using Tempest.Core.Scaffolding.Transforms;

namespace Tempest.Core.Scaffolding.Operations
{
    public class EmptyOperation : Operation
    {
        public EmptyOperation() : base(EmptyStreamSource.Instance, EmptyStreamTransformer.Instance, EmptyPersister.Instance)
        {
        }
    }
}