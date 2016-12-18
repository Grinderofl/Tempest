using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Scaffolding.Persistence;

namespace Tempest.Core.Setup.Persistence
{
    public class GlobFunctioningFilePersisterFactory : PersisterFactory
    {
        private readonly Func<string, string> _func;

        public GlobFunctioningFilePersisterFactory(Func<string, string> func)
        {
            _func = func;
        }

        protected override string GetPersistenceTarget()
        {
            return "Glob";
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            // Hack
            if (context.Filename.StartsWith("\\"))
                context.Filename = context.Filename.Substring(1);
            var path = _func(context.Filename);
            if (path.StartsWith("\\"))
                path = path.Substring(1);
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, path);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            yield return new FilePersister(absolutePath);
        }
    }
}