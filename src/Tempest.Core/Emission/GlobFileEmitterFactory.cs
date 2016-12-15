using System;
using System.IO;

namespace Tempest.Core.Emission
{
    // TODO Lots of inheritance possibilities here
    public class GlobFileEmitterFactory : EmitterFactory
    {
        private readonly string _globPath;

        public GlobFileEmitterFactory(string globPath)
        {
            if (globPath == null) throw new ArgumentNullException(nameof(globPath));
            _globPath = globPath;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            // Hack
            if (context.Filename.StartsWith("\\"))
                context.Filename = context.Filename.Substring(1);
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