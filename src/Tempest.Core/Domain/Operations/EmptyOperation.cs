using System.IO;
using Tempest.Core.Domain.Streaming;
using Tempest.Core.Emission;

namespace Tempest.Core.Domain.Operations
{
    public class EmptyOperation : Operation
    {
        public EmptyOperation() : base(EmptyStreamFactory.Instance, stream => stream, EmptyEmitter.Instance)
        {
        }
    }
}