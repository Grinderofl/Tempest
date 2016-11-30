using System;
using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Emission
{
    /// <summary>
    /// Emits the input stream into ... wherever
    /// Could be:
    /// FileEmitter
    /// GithubEmitter
    /// FtpUploadEmitter
    /// DeleteFileEmitter? No, that's wrong, should file deletes even be possible? Maybe template has command 'undo'? :P no, maybe the template needs to delete something in order to use another feature? idk!
    /// </summary>
    public abstract class Emitter
    {
        public virtual EmissionResult Emit(EmissionContext context)
        {
            return new EmissionResult();
        }

    }

    public class Emitters
    {
        public static Emitter ToFile(string relativePath) => new FileEmitter(relativePath);
        public static Emitter ToFile(Func<string> relativePathFunc) => new FileExpressionEmitter(relativePathFunc);
    }

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

    /// <summary>
    /// Emits to file
    /// </summary>
    public class FileEmitter : Emitter
    {
        private readonly string _relativePath;

        public FileEmitter(string relativePath)
        {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));
            _relativePath = relativePath;
        }

        public override EmissionResult Emit(EmissionContext context)
        {
            var absolutePath = Path.Combine(context.TargetDirectory.FullName, _relativePath);
            using (var fs = File.Create(absolutePath))
            {
                context.InputStream.Seek(0, SeekOrigin.Begin);
                context.InputStream.CopyTo(fs);
            }
            return new EmissionResult() ;
            //using (var fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
            //{
            //    using (var writer = new StreamWriter(fs))
            //    {
                    
            //    }
            //}
            
        }
    }
}