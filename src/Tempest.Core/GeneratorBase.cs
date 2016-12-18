using System.IO;
using Tempest.Core.Setup.OperationBuilding;

namespace Tempest.Core
{
    // TODO Api needs rework. More abstractions...
    public abstract class GeneratorBase : GeneratorEngineBase
    {
        private string _targetSubDirectory;

        protected GeneratorBase()
        {
            Options = new OptionsFactory(this);
            Update = new UpdateOperationBuilder(this);
            Copy = new CopyOperationBuilder(this);
            Create = new CreateOperationBuilder(this);
            Set = new SetOperationBuilder(this);
            Globally = new GlobalOperationBuilder(this);
        }

        /// <summary>
        ///     Creates a new file
        /// </summary>
        public CreateOperationBuilder Create { get; set; }

        /// <summary>
        ///     Updates an existing file in the target directory
        /// </summary>
        public UpdateOperationBuilder Update { get; set; }

        /// <summary>
        ///     Copies a file from templates
        /// </summary>
        public CopyOperationBuilder Copy { get; set; }

        /// <summary>
        ///     Sets some internal variables
        /// </summary>
        public SetOperationBuilder Set { get; set; }

        /// <summary>
        ///     Creates some configuration options
        /// </summary>
        public OptionsFactory Options { get; set; }

        /// <summary>
        ///     Globally uses transformers or emitters (executed for every source)
        /// </summary>
        public GlobalOperationBuilder Globally { get; set; }

        protected override DirectoryInfo BuildTemplatePath(GeneratorContext generatorContext)
            =>
            new DirectoryInfo(Path.Combine(generatorContext.TemplateDirectory.FullName,
                BuildGeneratorTemplateDirectory(generatorContext.GeneratorName)));

        private string BuildGeneratorTemplateDirectory(string generatorName)
            => $"Generators/Tempest.Generator.{generatorName}/Template";

        protected override DirectoryInfo BuildTargetPath(GeneratorContext generatorContext)
            => new DirectoryInfo(Path.Combine(generatorContext.WorkingDirectory.FullName, _targetSubDirectory));

        /// <summary>
        ///     Sets the directory which to use as the target directory
        ///     relative to the current working path
        /// </summary>
        /// <example>
        ///     If the directory you executed Tempest from is 'C:\Projects\' and you set the
        ///     target subdirectory to 'Foo', then the generator will be working in 'C:\Projects\Foo\'
        /// </example>
        /// <param name="relativePath"></param>
        public virtual void SetTargetSubDirectory(string relativePath) => _targetSubDirectory = relativePath;
    }
}