using System;
using System.Collections.Generic;
using System.IO;

namespace Tempest.Core.Emission
{
    /// <summary>
    ///     Emits to file
    /// </summary>
    public class FileEmitterFactory : EmitterFactory
    {
        private readonly string _relativePath;

        public FileEmitterFactory(string relativePath)
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
            return new EmissionResult();
        }

        protected override string GetEmissionTarget()
        {
            return _relativePath;
        }

        public override IEnumerable<ActualEmitter> CreateEmitters(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(absolutePath));
            yield return new ActualFileEmitter(absolutePath);
        }
    }
}