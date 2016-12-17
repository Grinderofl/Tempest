using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public interface IStreamPersister
    {
        void Persist(Stream sourceStream);
    }
}