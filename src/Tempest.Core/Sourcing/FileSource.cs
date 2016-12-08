using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Sourcing
{
    public abstract class FileSource : Source
    {
        private readonly string _relativePath;

        protected FileSource(string relativePath)
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
                OutputStream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read),
                FilePath = ""
            };
        }

        protected abstract string AssembleFilePath(SourcingContext context, string relativePath);
    }
}