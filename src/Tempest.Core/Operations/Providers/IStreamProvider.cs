using System.IO;

namespace Tempest.Core.Operations.Providers
{
    public interface IStreamProvider
    {
        string Describe();
        Stream Provide();
    }
}