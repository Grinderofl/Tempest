using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public class VoidPersister : AbstractStreamPersister
    {
        public static AbstractStreamPersister Instance => new VoidPersister();

        private VoidPersister()
        { }

        public override void Persist(Stream sourceStream)
        {
            // Ka-ching!
        }

        protected override string GetStreamDescriptor()
        {
            return $"null";
        }
    }
}