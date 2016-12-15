using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Emission
{
    public class GlobFileExpressionEmitterFactory : EmitterFactory
    {
        private readonly Func<string> _globPathFunc;

        public GlobFileExpressionEmitterFactory(Func<string> globPathFunc)
        {
            _globPathFunc = globPathFunc;
        }

        //public override EmissionResult Emit(EmissionContext context)
        //{
        //    var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPathFunc(), context.Filename);
        //    using (var fs = File.Create(absolutePath))
        //    {
        //        context.EmissionStream.Seek(0, SeekOrigin.Begin);
        //        context.EmissionStream.CopyTo(fs);
        //    }
        //    return new EmissionResult();
        //}

        protected override string GetEmissionTarget()
        {
            return _globPathFunc();
        }

        public override IEnumerable<ActualEmitter> CreateEmitters(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _globPathFunc(), context.Filename);
            yield return new ActualFileEmitter(absolutePath);
        }
    }
}