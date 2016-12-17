using System.Collections.Generic;
using System.IO;
using Tempest.Core.Scaffolding.Sources;

namespace Tempest.Core.Setup.Sourcing
{
    public abstract class FileSourceFactory : SourceFactory
    {
        private readonly string _relativePath;

        protected FileSourceFactory(string relativePath)
        {
            _relativePath = relativePath;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var absolutePath = AssembleFilePath(context, _relativePath);
            if (!File.Exists(absolutePath))
                throw new FileNotFoundException($"The specified file '{absolutePath}' could not be found.");

            var fileName = Path.GetFileName(absolutePath);
            yield return new SourcingResult
            {
                FileName = fileName,
                Source = new OpenFileStreamSource(absolutePath),
                FilePath = absolutePath
            };
        }

        protected abstract string AssembleFilePath(SourcingContext context, string relativePath);
    }
}