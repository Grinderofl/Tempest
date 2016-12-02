using System;
using Tempest.Core.Emission;
using Tempest.Core.Sourcing;

namespace Tempest.Core.Setup
{
    public class TemplateStep : TemplateStepBase
    {
        public TemplateStep(Source source) : base(source)
        {
        }

        /// <summary>
        /// Emits the final output to the provided emitter
        /// </summary>
        /// <param name="emitter"></param>
        /// <returns></returns>
        public TemplateStep To(Emitter emitter)
        {
            InternalEmitters.Add(emitter);
            return this;
        }

        /// <summary>
        /// Emits the final output to the specified file relative to the target directory
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <returns></returns>
        public TemplateStep ToFile(string relativeFilePath) => To(Emitters.ToFile(relativeFilePath));

        /// <summary>
        /// Emits the final output to the specified file function relative to the target directory
        /// </summary>
        /// <param name="relativeFilePathFunc"></param>
        /// <returns></returns>
        public TemplateStep ToFile(Func<string> relativeFilePathFunc) => To(Emitters.ToFile(relativeFilePathFunc));

    }
}