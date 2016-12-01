using System;
using System.IO;

namespace Tempest.Core.Emission
{
    /// <summary>
    /// Emits to file
    /// </summary>
    public class FileEmitter : Emitter
    {
        private readonly string _relativePath;

        public FileEmitter(string relativePath)
        {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));
            _relativePath = relativePath;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePath);
            using (var fs = File.Create(absolutePath))
            {
                context.InputStream.Seek(0, SeekOrigin.Begin);
                context.InputStream.CopyTo(fs);
            }
            return new EmissionResult() ;
            //using (var fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
            //{
            //    using (var writer = new StreamWriter(fs))
            //    {
                    
            //    }
            //}
            
        }
    }
}