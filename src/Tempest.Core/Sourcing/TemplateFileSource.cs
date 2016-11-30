using System;
using System.IO;

namespace Tempest.Core.Sourcing
{
    public class TemplateFileSource : Source
    {
        private readonly string _relativePath;

        /// <summary>
        /// Initializes a new instance of TemplateFileSource.
        /// </summary>
        /// <param name="relativePath">The relative path of the file to the template root</param>
        public TemplateFileSource(string relativePath)
        {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));
            _relativePath = relativePath;
        }

        protected override Stream GenerateCore(SourcingContext context)
        {
            var absolutePath = Path.Combine(context.TemplateRoot.FullName, _relativePath);
            if(!File.Exists(absolutePath))
                throw new FileNotFoundException($"The specified template file '{absolutePath}' could not be found.");

            return new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
        }
    }
}