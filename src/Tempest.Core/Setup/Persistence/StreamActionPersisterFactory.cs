using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Setup.Persistence
{
    public class StreamActionPersisterFactory : PersisterFactory
    {
        private readonly Action<Stream> _targetStream;

        public StreamActionPersisterFactory(Action<Stream> targetStream)
        {
            _targetStream = targetStream;
        }

        protected override string GetPersistenceTarget()
        {
            return "[New stream]";
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            yield return new ActionStreamPersister(_targetStream);
        }
    }
}