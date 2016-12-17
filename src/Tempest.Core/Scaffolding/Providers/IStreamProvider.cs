using System.IO;

namespace Tempest.Core.Scaffolding.Providers
{
    public interface IStreamProvider
    {
        string Describe();
        Stream Provide();
    }
}