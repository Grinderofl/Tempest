using System;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Setup.Persisters;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core.OperationBuilding
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
        public OperationStep ToFile(string relativeFilePath) => To(Persisters.ToFile(relativeFilePath));

        /// <summary>
        ///     Emits the final output to the specified file function relative to the target directory
        /// </summary>
        /// <param name="relativeFilePathFunc"></param>
        /// <returns></returns>
        public OperationStep ToFile(Func<string> relativeFilePathFunc) => To(Persisters.ToFile(relativeFilePathFunc));

        public OperationStep ToFiles() => To(Persisters.ToFiles());
        public OperationStep ToFiles(string fileGlob) => To(Persisters.ToFiles(fileGlob));
        public OperationStep ToFiles(Func<string> fileGlobFunc) => To(Persisters.ToFiles(fileGlobFunc));
        public OperationStep ToFiles(Func<string, string> fileNameTransformationFunc)
            => To(Persisters.ToFiles(fileNameTransformationFunc));


        public OperationStep Using(OperationTransformer operationTransformer)
        {
            InternalTransformers.Add(operationTransformer);
            return this;
        }

        public OperationStep ReplaceToken(string token, string replaceWith)
            => Using(Transformers.Token(token, replaceWith));


    }
}