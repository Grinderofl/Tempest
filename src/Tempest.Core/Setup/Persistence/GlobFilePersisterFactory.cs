using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Setup.Persistence
{
    // TODO Lots of inheritance possibilities here
    public class GlobFilePersisterFactory : PersisterFactory
    {
        private readonly string _globPath;

        public GlobFilePersisterFactory(string globPath)
        {
            if (globPath == null) throw new ArgumentNullException(nameof(globPath));
            _globPath = globPath;
        }

        protected override string GetPersistenceTarget()
        {
            return _globPath;
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            if (context.Filename.StartsWith("\\"))
                context.Filename = context.Filename.Substring(1);
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPath, context.Filename);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            yield return new FilePersister(absolutePath);
        }
    }
}