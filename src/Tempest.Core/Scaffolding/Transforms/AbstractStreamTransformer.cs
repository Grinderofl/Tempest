using System.IO;

namespace Tempest.Core.Scaffolding.Transforms
{
    public abstract class AbstractStreamTransformer : IStreamTransformer
    {
        public abstract Stream Transform(Stream stream);
    }
}