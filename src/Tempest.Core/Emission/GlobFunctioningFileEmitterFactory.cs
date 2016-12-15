using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Emission
{
    public class GlobFunctioningFileEmitterFactory : EmitterFactory
    {
        private readonly Func<string, string> _func;

        public GlobFunctioningFileEmitterFactory(Func<string, string> func)
        {
            _func = func;
        }

        //public override EmissionResult Emit(EmissionContext context)
        //{
        //    // Hack
        //    if (context.Filename.StartsWith("\\"))
        //        context.Filename = context.Filename.Substring(1);
        //    var path = _func(context.Filename);
        //    if (path.StartsWith("\\"))
        //        path = path.Substring(1);
        //    var absolutePath = Path.Combine(context.TargetDirectory.FullName, path);
        //    Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
        //    using (var fs = File.Create(absolutePath))
        //    {
        //        context.EmissionStream.Seek(0, SeekOrigin.Begin);
        //        context.EmissionStream.CopyTo(fs);
        //    }
        //    return new EmissionResult();
        //}

        protected override string GetEmissionTarget()
        {
            return "Glob";
        }

        public override IEnumerable<ActualEmitter> CreateEmitters(EmissionContext context)
        {
            // Hack
            if (context.Filename.StartsWith("\\"))
                context.Filename = context.Filename.Substring(1);
            var path = _func(context.Filename);
            if (path.StartsWith("\\"))
                path = path.Substring(1);
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, path);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            yield return new ActualFileEmitter(absolutePath);
        }
    }
}