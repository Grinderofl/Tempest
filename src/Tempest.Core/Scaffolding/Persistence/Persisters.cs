using System;
using Tempest.Core.Setup.Persisters;

namespace Tempest.Core.Scaffolding.Persistence
{
    public class Persisters
    {
        public static PersisterFactory ToFile(string relativePath) => new FilePersisterFactory(relativePath);
        public static PersisterFactory ToFile(Func<string> relativePathFunc) => new FileExpressionPersisterFactory(relativePathFunc);
        public static PersisterFactory ToFiles(string globPattern) => new GlobFilePersisterFactory(globPattern);
        public static PersisterFactory ToFiles(Func<string> globPatternFunc) => new GlobFileExpressionPersisterFactory(globPatternFunc);
        public static PersisterFactory ToFiles() => new GlobFilePersisterFactory("");

        public static PersisterFactory ToFiles(Func<string, string> fileNameTransformationFunc) => new GlobFunctioningFilePersisterFactory(fileNameTransformationFunc);
    }
}