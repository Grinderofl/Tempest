using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public interface IStreamPersister
    {
        string Describe();
        void Persist(Stream sourceStream);
    }
}