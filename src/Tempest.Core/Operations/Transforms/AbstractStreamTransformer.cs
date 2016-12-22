using System.IO;

namespace Tempest.Core.Operations.Transforms
{
    public abstract class AbstractStreamTransformer : IStreamTransformer
    {
        public abstract Stream Transform(Stream stream);
        public virtual string Describe()
        {
            return $"{GetType()}";
        }
    }
}