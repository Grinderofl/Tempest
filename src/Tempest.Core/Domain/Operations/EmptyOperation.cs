using System.IO;
using Tempest.Core.Emission;

namespace Tempest.Core.Domain.Operations
{
    public class EmptyOperation : Operation
    {
        public EmptyOperation() : base(new MemoryStream(), stream => stream, EmptyEmitter.Instance)
        {
        }
    }
}