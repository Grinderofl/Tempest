using System;
using System.IO;
using Tempest.Core.Scaffolding;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Setup.Persistence;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.Setup.OperationBuilding
{
    public class OperationStep : OperationStepBase
    {
        public OperationStep(SourceFactory sourceFactory) : base(sourceFactory)
        {
        }

        /// <summary>
        ///     Emits the final output to the provided emitter
        /// </summary>
        /// <param name="persisterFactory"></param>
        /// <returns></returns>
        public OperationStep To(PersisterFactory persisterFactory)
        {
            InternalEmitters.Add(persisterFactory);
            return this;
        }

        /// <summary>
        ///     Emits the final output to the specified file relative to the target directory
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public OperationStep ToFile(string relativeFilePath) => To(Scaffolding.Persistence.Persisters.ToFile(relativeFilePath));

        /// <summary>
        ///     Emits the final output to the specified file function relative to the target directory
        /// </summary>
        /// <param name="relativeFilePathFunc"></param>
        /// <returns></returns>
        public OperationStep ToFile(Func<string> relativeFilePathFunc) => To(Scaffolding.Persistence.Persisters.ToFile(relativeFilePathFunc));

        public OperationStep ToFiles() => To(Scaffolding.Persistence.Persisters.ToFiles());
        public OperationStep ToFiles(string fileGlob) => To(Scaffolding.Persistence.Persisters.ToFiles(fileGlob));
        public OperationStep ToFiles(Func<string> fileGlobFunc) => To(Scaffolding.Persistence.Persisters.ToFiles(fileGlobFunc));
        public OperationStep ToFiles(Func<string, string> fileNameTransformationFunc)
            => To(Scaffolding.Persistence.Persisters.ToFiles(fileNameTransformationFunc));

        public OperationStep ToStream(Stream targetStream) => To(Persisters.ToStream(targetStream));
        public OperationStep ToStream(Action<Stream> streamAction) => To(Persisters.ToStream(streamAction));

        public OperationStep Using(OperationTransformer operationTransformer)
        {
            InternalTransformers.Add(operationTransformer);
            return this;
        }

        public OperationStep ReplaceToken(string token, string replaceWith)
            => Using(Transformers.Token(token, replaceWith));


    }
}