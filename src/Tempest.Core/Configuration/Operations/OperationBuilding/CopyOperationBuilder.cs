using System.Reflection;
using Tempest.Core.Configuration.Operations.Sourcing;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    /// <summary>
    ///     Contains methods that should copy a template file
    /// </summary>
    public class CopyOperationBuilder : OperationBuilderBase
    {

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
        /// <param name="assembly"></param>
        /// <returns></returns>
        public OperationStep Resource(string resourcePath, Assembly assembly)
            => CreateStep(Sources.FromResource(resourcePath, assembly));

        public OperationStep ResourceOf<T>(string resourcePath) => CreateStep(Sources.FromResourceOf<T>(resourcePath));
    }
}