using System.IO;

namespace Tempest.Core.Scaffolding.Persistence
{
    public class StreamPersister : AbstractStreamPersister
    {
        private readonly Stream _destinationStream;

        public StreamPersister(Stream destinationStream)
        {
            _destinationStream = destinationStream;
        }

        public override void Persist(Stream sourceStream)
        {
            sourceStream.Seek(0, SeekOrigin.Begin);
            sourceStream.CopyTo(_destinationStream);
        }

        protected override string GetStreamDescriptor()
        {
            return $"[New stream]";
        }
    }
}