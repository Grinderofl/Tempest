using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Scaffolding.Persistence;

namespace Tempest.Core.Setup.Persistence
{
    /// <summary>
    ///     Emits to file
    /// </summary>
    public class FilePersisterFactory : PersisterFactory
    {
        private readonly string _relativePath;

        public FilePersisterFactory(string relativePath)
        {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));
            _relativePath = relativePath;
        }

        protected override string GetPersistenceTarget()
        {
            return _relativePath;
        }

        public override IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            yield return new FilePersister(absolutePath);
        }
    }
}