using System.IO;

namespace Tempest.Core.Scaffolding.Transforms
{
    public interface IStreamTransformer
    {
        Stream Transform(Stream stream);
    }
}