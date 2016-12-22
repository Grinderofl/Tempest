using System.IO;

namespace Tempest.Core.Operations.Transforms
{
    public interface IStreamTransformer
    {
        Stream Transform(Stream stream);
        string Describe();
    }
}