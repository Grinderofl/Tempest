using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Tempest.Core.Sourcing
{
    // Lots of inheritance possibilities here
    public class TemplateFileSource : Source
    {
        private readonly string _relativePath;

        /// <summary>
        ///     Initializes a new instance of TemplateFileSource.
        /// </summary>
        /// <param name="relativePath">The relative path of the file to the template root</param>
        public TemplateFileSource(string relativePath)
        {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));
            _relativePath = relativePath;
        }

        public override IEnumerable<SourcingResult> Generate(SourcingContext context)
        {
            var absolutePath = Path.Combine(context.TemplateRoot.FullName, _relativePath);
            if (!File.Exists(absolutePath))
                throw new FileNotFoundException($"The specified template file '{absolutePath}' could not be found.");

            var fileName = Path.GetFileName(absolutePath);

            yield return new SourcingResult
            {
                FileName = fileName,
                OutputStream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read)
            };
        }
    }
}