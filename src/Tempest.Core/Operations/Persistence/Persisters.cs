using System;
using System.IO;
using Tempest.Core.Configuration.Operations.Persistence;

namespace Tempest.Core.Operations.Persistence
{
    public class Persisters
    {
        public static PersisterFactoryBase ToFile(string relativePath) => new FilePersisterFactory(relativePath);
        public static PersisterFactoryBase ToFile(Func<string> relativePathFunc) => new FileExpressionPersisterFactory(relativePathFunc);
        public static PersisterFactoryBase ToFiles(string globPattern) => new GlobFilePersisterFactory(globPattern);
        public static PersisterFactoryBase ToFiles(Func<string> globPatternFunc) => new GlobFileExpressionPersisterFactory(globPatternFunc);
        public static PersisterFactoryBase ToFiles() => new GlobFilePersisterFactory("");
        public static PersisterFactoryBase ToFiles(Func<string, string> fileNameTransformationFunc) => new GlobFunctioningFilePersisterFactory(fileNameTransformationFunc);
        public static PersisterFactoryBase ToStream(Stream targetStream) => new StreamPersisterFactory(targetStream);
        public static PersisterFactoryBase ToStream(Action<Stream> targetStreamAction) => new StreamActionPersisterFactory(targetStreamAction);
    }
}