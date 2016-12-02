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
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            using (var fs = File.Create(absolutePath))
            {
                context.EmissionStream.Seek(0, SeekOrigin.Begin);
                context.EmissionStream.CopyTo(fs);
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