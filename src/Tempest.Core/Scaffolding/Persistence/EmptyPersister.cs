using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public class EmptyPersister : AbstractStreamPersister
    {
        public static AbstractStreamPersister Instance => new EmptyPersister();

        private EmptyPersister()
        { }

        public override void Persist(Stream sourceStream)
        {
            // Ka-ching!
        }
    }
}