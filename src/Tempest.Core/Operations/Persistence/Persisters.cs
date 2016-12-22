using System;
using System.IO;
using Tempest.Core.Configuration.Operations.Persistence;

namespace Tempest.Core.Operations.Persistence
{
    public class Persisters
    {
        public static PersisterFactory ToFile(string relativePath) => new FilePersisterFactory(relativePath);
        public static PersisterFactory ToFile(Func<string> relativePathFunc) => new FileExpressionPersisterFactory(relativePathFunc);
        public static PersisterFactory ToFiles(string globPattern) => new GlobFilePersisterFactory(globPattern);
        public static PersisterFactory ToFiles(Func<string> globPatternFunc) => new GlobFileExpressionPersisterFactory(globPatternFunc);
        public static PersisterFactory ToFiles() => new GlobFilePersisterFactory("");
        public static PersisterFactory ToFiles(Func<string, string> fileNameTransformationFunc) => new GlobFunctioningFilePersisterFactory(fileNameTransformationFunc);
        public static PersisterFactory ToStream(Stream targetStream) => new StreamPersisterFactory(targetStream);
        public static PersisterFactory ToStream(Action<Stream> targetStreamAction) => new StreamActionPersisterFactory(targetStreamAction);
    }
}