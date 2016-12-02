using System.IO;
using Tempest.Core.Setup;

namespace Tempest.Core
{
    public abstract class GeneratorBase : GeneratorEngineBase
    {
        private string _targetSubDirectory;

        protected GeneratorBase()
        {
            Options = new OptionsFactory(this);
            Update = new UpdateFactory(this);
            Copy = new CopyFactory(this);
            Create = new CreateFactory(this);
            Set = new SetFactory(this);
            Globally = new GlobalFactory(this);
        }

        /// <summary>
        /// Creates a new file
        /// </summary>
        public CreateFactory Create { get; set; }

        /// <summary>
        /// Updates an existing file in the target directory
        /// </summary>
        public UpdateFactory Update { get; set; }

        /// <summary>
        /// Copies a file from templates
        /// </summary>
        public CopyFactory Copy { get; set; }

        /// <summary>
        /// Sets some internal variables
        /// </summary>
        public SetFactory Set { get; set; }

        /// <summary>
        /// Creates some configuration options
        /// </summary>
        public OptionsFactory Options { get; set; }

        /// <summary>
        /// Globally uses transformers or emitters (executed for every source)
        /// </summary>
        public GlobalFactory Globally { get; set; }

        protected override DirectoryInfo BuildTemplatePath(RunnerContext runnerContext)
            =>
            new DirectoryInfo(Path.Combine(runnerContext.TempestDirectory.FullName,
                BuildGeneratorTemplateDirectory(runnerContext.GeneratorName)));

        private string BuildGeneratorTemplateDirectory(string generatorName)
            => $"Generators/Tempest.Generator.{generatorName}/Template";

        protected override DirectoryInfo BuildTargetPath(RunnerContext runnerContext)
            => new DirectoryInfo(Path.Combine(runnerContext.WorkingDirectory.FullName, _targetSubDirectory));

        /// <summary>
        /// Sets the directory which to use as the target directory
        /// relative to the current working path
        /// </summary>
        /// <example>
        /// If the directory you executed Tempest from is 'C:\Projects\' and you set the
        /// target subdirectory to 'Foo', then the generator will be working in 'C:\Projects\Foo\'
        /// </example>
        /// <param name="relativePath"></param>
        public virtual void SetTargetSubDirectory(string relativePath) => _targetSubDirectory = relativePath;
    }
}