using System.Diagnostics;
using System.IO;

namespace Tempest.Core.Emission
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class ActualEmitter
    {
        public virtual string DebuggerDisplay()
        {
            return $"{GetType()}";
        }

        public abstract void Emit(Stream sourceStream);
    }
}