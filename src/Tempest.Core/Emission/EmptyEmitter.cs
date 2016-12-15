using System.IO;

namespace Tempest.Core.Emission
{
    public class EmptyEmitter : ActualEmitter
    {
        public static ActualEmitter Instance => new EmptyEmitter();

        private EmptyEmitter()
        { }

        public override void Emit(Stream sourceStream)
        {
            // Ka-ching!
        }
    }
}