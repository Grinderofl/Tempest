using System;

namespace Tempest.Core.Emission
{
    public class Emitters
    {
        public static EmitterFactory ToFile(string relativePath) => new FileEmitterFactory(relativePath);
        public static EmitterFactory ToFile(Func<string> relativePathFunc) => new FileExpressionEmitterFactory(relativePathFunc);
        public static EmitterFactory ToFiles(string globPattern) => new GlobFileEmitterFactory(globPattern);
        public static EmitterFactory ToFiles(Func<string> globPatternFunc) => new GlobFileExpressionEmitterFactory(globPatternFunc);
        public static EmitterFactory ToFiles() => new GlobFileEmitterFactory("");

        public static EmitterFactory ToFiles(Func<string, string> fileNameTransformationFunc) => new GlobFunctioningFileEmitterFactory(fileNameTransformationFunc);
    }
}