using System;
using System.IO;

namespace Tempest.Core.Operations.Persistence
{
    public class ActionStreamPersister : AbstractStreamPersister
    {
        private readonly Action<Stream> _streamAction;

        public ActionStreamPersister(Action<Stream> streamAction)
        {
            _streamAction = streamAction;
        }

        protected override string GetStreamDescriptor()
        {
            return $"[New action stream]";
        }

        public override void Persist(Stream sourceStream)
        {
            _streamAction(sourceStream);
        }
    }
}