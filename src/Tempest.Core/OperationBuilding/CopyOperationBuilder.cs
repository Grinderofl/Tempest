using System.Reflection;
using Tempest.Core.Setup.Sourcing;

namespace Tempest.Core.OperationBuilding
{
    /// <summary>
    ///     Contains methods that should copy a template file
    /// </summary>
    public class CopyOperationBuilder : OperationBuilderBase
    {
        public CopyOperationBuilder(GeneratorBase engine) : base(engine)
        {
        }

        /// <summary>
        ///     Copy a template to your target directory
        /// </summary>
        /// <param name="filePath">
        ///     Relative path to your template file, located under the template folder which defaults to
        ///     './Template/'
        /// </param>
        /// <returns></returns>
        public OperationStep Template(string filePath) => CreateStep(Sources.FromTemplate(filePath));

        public OperationStep TemplatePattern(string glob) => CreateStep(Sources.FromTemplateGlob(glob));

        /// <summary>
        ///     Copies a template resource
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public OperationStep Resource(string resourcePath)
            => CreateStep(Sources.FromResource(resourcePath, Engine.GetType().GetTypeInfo().Assembly));
    }
}