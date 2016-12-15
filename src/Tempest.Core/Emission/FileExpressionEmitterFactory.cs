using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Emission
{
    // TODO Lots of inheritance possibilities here
    public class FileExpressionEmitterFactory : EmitterFactory
    {
        private readonly Func<string> _relativePathFunc;

        public FileExpressionEmitterFactory(Func<string> relativePathFunc)
        {
            _relativePathFunc = relativePathFunc;
        }

        //public override EmissionResult Emit(EmissionContext context)
        //{
        //    var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePathFunc());
        //    var directoryPath = new FileInfo(absolutePath).Directory.FullName;
        //    if (!Directory.Exists(directoryPath))
        //        Directory.CreateDirectory(directoryPath);
        //    using (var fs = File.Create(absolutePath))
        //    {
        //        context.EmissionStream.Seek(0, SeekOrigin.Begin);
        //        context.EmissionStream.CopyTo(fs);
        //    }
        //    return new EmissionResult();
        //}

        protected override string GetEmissionTarget()
        {
            return _relativePathFunc();
        }

        public override IEnumerable<ActualEmitter> CreateEmitters(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePathFunc());
            var directoryPath = new FileInfo(absolutePath).Directory.FullName;
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            yield return new ActualFileEmitter(absolutePath);
        }
    }
}