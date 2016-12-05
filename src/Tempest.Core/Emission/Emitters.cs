using System;

namespace Tempest.Core.Emission
{
    public class Emitters
    {
        public static Emitter ToFile(string relativePath) => new FileEmitter(relativePath);
        public static Emitter ToFile(Func<string> relativePathFunc) => new FileExpressionEmitter(relativePathFunc);
        public static Emitter ToFiles(string globPattern) => new GlobFileEmitter(globPattern);
        public static Emitter ToFiles(Func<string> globPatternFunc) => new GlobFileExpressionEmitter(globPatternFunc);
        public static Emitter ToFiles() => new GlobFileEmitter("");
    }
}