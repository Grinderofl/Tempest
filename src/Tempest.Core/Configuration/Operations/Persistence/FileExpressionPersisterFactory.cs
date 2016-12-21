using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Configuration.Operations.Persistence
{
    // TODO Lots of inheritance possibilities here
    public class FileExpressionPersisterFactory : PersisterFactory
    {
        private readonly Func<string> _relativePathFunc;

        public FileExpressionPersisterFactory(Func<string> relativePathFunc)
        {
            _relativePathFunc = relativePathFunc;
        }

        protected override string GetPersistenceTarget()
        {
            return _relativePathFunc();
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePathFunc());
            var directoryPath = new FileInfo(absolutePath).Directory.FullName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            yield return new FilePersister(absolutePath);
        }
    }
}