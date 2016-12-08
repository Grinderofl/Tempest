using System;
using System.IO;

namespace Tempest.Core.Emission
{
    public class FileExpressionEmitter : Emitter
    {
        private readonly Func<string> _relativePathFunc;

        public FileExpressionEmitter(Func<string> relativePathFunc)
        {
            _relativePathFunc = relativePathFunc;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePathFunc());
            var directoryPath = new FileInfo(absolutePath).Directory.FullName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            using (var fs = File.Create(absolutePath))
            {
                context.EmissionStream.Seek(0, SeekOrigin.Begin);
                context.EmissionStream.CopyTo(fs);
            }
            return new EmissionResult();
        }
    }
}