using System;

namespace Tempest.Core.Emission
{
    public class Emitters
    {
        public static Emitter ToFile(string relativePath) => new FileEmitter(relativePath);
        public static Emitter ToFile(Func<string> relativePathFunc) => new FileExpressionEmitter(relativePathFunc);
    }
}