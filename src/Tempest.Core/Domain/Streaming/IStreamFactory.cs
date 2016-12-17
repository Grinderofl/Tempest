using System.IO;

namespace Tempest.Core.Domain.Streaming
{
    public interface IStreamFactory
    {
        Stream Create();
    }
}