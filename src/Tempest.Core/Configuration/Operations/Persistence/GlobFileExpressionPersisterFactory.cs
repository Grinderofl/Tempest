using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Configuration.Operations.Persistence
{
    public class GlobFileExpressionPersisterFactory : PersisterFactoryBase
    {
        private readonly Func<string> _globPathFunc;

        public GlobFileExpressionPersisterFactory(Func<string> globPathFunc)
        {
            _globPathFunc = globPathFunc;
        }

        protected override string GetPersistenceTarget()
        {
            return _globPathFunc();
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPathFunc(), context.Filename);
            yield return new FilePersister(absolutePath);
        }
    }
}