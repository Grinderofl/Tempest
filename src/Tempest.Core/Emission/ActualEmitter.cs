using System.Diagnostics;
using System.IO;

namespace Tempest.Core.Emission
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class ActualEmitter : IStreamEmitter
    {
        public virtual string DebuggerDisplay()
        {
            return $"{GetType()}";
        }

        public abstract void Emit(Stream sourceStream);
    }

    public interface IStreamEmitter
    {
        void Emit(Stream sourceStream);
    }
}