using System;
using Tempest.Core.Emission;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core.Setup
{
    public class ScaffoldStep : TemplateStepBase
    {
        public ScaffoldStep(Source source) : base(source)
        {
        }

        /// <summary>
        ///     Emits the final output to the provided emitter
        /// </summary>
        /// <param name="emitter"></param>
        /// <returns></returns>
        public ScaffoldStep To(Emitter emitter)
        {
            InternalEmitters.Add(emitter);
            return this;
        }

        /// <summary>
        ///     Emits the final output to the specified file relative to the target directory
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public ScaffoldStep ToFile(string relativeFilePath) => To(Emitters.ToFile(relativeFilePath));

        /// <summary>
        ///     Emits the final output to the specified file function relative to the target directory
        /// </summary>
        /// <param name="relativeFilePathFunc"></param>
        /// <returns></returns>
        public ScaffoldStep ToFile(Func<string> relativeFilePathFunc) => To(Emitters.ToFile(relativeFilePathFunc));

        public ScaffoldStep ToFiles() => To(Emitters.ToFiles());
        public ScaffoldStep ToFiles(string fileGlob) => To(Emitters.ToFiles(fileGlob));
        public ScaffoldStep ToFiles(Func<string> fileGlobFunc) => To(Emitters.ToFiles(fileGlobFunc));
        public ScaffoldStep ToFiles(Func<string, string> fileNameTransformationFunc)
            => To(Emitters.ToFiles(fileNameTransformationFunc));


        public ScaffoldStep Using(Transformer transformer)
        {
            InternalTransformers.Add(transformer);
            return this;
        }

        public ScaffoldStep ReplaceToken(string token, string replaceWith)
            => Using(Transformers.Token(token, replaceWith));


    }
}