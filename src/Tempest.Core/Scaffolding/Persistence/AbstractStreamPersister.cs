using System.Diagnostics;
using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class AbstractStreamPersister : IStreamPersister
    {
        protected virtual string DebuggerDisplay()
        {
            return $"{GetType()}";
        }

        public abstract void Persist(Stream sourceStream);
    }
}