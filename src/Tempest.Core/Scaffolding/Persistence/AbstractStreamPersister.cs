using System.Diagnostics;
using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    [DebuggerDisplay("{Describe()}")]
    public abstract class AbstractStreamPersister : IStreamPersister
    {
        protected abstract string GetStreamDescriptor();

        public abstract void Persist(Stream sourceStream);
        public virtual string Describe()
        {
            return $"{GetStreamDescriptor()} {GetType()}";
        }
    }
}