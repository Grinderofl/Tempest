using System;
using System.IO;

namespace Tempest.Core.Emission
{
    public class GlobFileEmitter : Emitter
    {
        private readonly string _globPath;

        public GlobFileEmitter(string globPath)
        {
            if (globPath == null) throw new ArgumentNullException(nameof(globPath));
            _globPath = globPath;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPath, context.Filename);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            using (var fs = File.Create(absolutePath))
            {
                context.EmissionStream.Seek(0, SeekOrigin.Begin);
                context.EmissionStream.CopyTo(fs);
            }
            return new EmissionResult();
        }
    }
}