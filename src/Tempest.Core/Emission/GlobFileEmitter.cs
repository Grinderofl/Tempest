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

    public class GlobFunctioningFileEmitter : Emitter
    {
        private readonly Func<string, string> _func;

        public GlobFunctioningFileEmitter(Func<string, string> func)
        {
            _func = func;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            // Hack
            if (context.Filename.StartsWith("\\"))
                context.Filename = context.Filename.Substring(1);
            var path = _func(context.Filename);
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, path);
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