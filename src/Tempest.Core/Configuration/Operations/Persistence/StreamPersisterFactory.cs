using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Configuration.Operations.Persistence
{
    public class StreamPersisterFactory : PersisterFactoryBase
    {
        private readonly Stream _targetStream;

        public StreamPersisterFactory(Stream targetStream)
        {
            if (targetStream == null) throw new ArgumentNullException(nameof(targetStream));
            _targetStream = targetStream;
        }

        protected override string GetPersistenceTarget()
        {
            return "[New stream]";
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            yield return new StreamPersister(_targetStream);
        }
    }
}