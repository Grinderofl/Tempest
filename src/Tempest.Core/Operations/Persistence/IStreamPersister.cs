using System.IO;

namespace Tempest.Core.Operations.Persistence
{
    public interface IStreamPersister
    {
        string Describe();
        void Persist(Stream sourceStream);
    }
}