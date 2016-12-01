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
            using (var fs = File.Create(absolutePath))
            {
                context.InputStream.Seek(0, SeekOrigin.Begin);
                context.InputStream.CopyTo(fs);
            }
            return new EmissionResult();
        }
    }
}