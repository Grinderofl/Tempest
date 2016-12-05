using System;
using System.IO;

namespace Tempest.Core.Emission
{
    public class GlobFileExpressionEmitter : Emitter
    {
        private readonly Func<string> _globPathFunc;

        public GlobFileExpressionEmitter(Func<string> globPathFunc)
        {
            _globPathFunc = globPathFunc;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPathFunc(), context.Filename);
            using (var fs = File.Create(absolutePath))
            {
                context.EmissionStream.Seek(0, SeekOrigin.Begin);
                context.EmissionStream.CopyTo(fs);
            }
            return new EmissionResult();
        }
    }
}