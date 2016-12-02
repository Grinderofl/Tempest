using System.IO;

namespace Tempest.Core
{
    public abstract class GeneratorBase : GeneratorEngineBase
    {
        private string _targetSubDirectory;

        protected GeneratorBase()
        {
            Update = new UpdateFactory(this);
            Copy = new CopyFactory(this);
            Create = new CreateFactory(this);
        }

        public CreateFactory Create { get; set; }
        public UpdateFactory Update { get; set; }
        public CopyFactory Copy { get; set; }

        public OptionsFactory Options { get; set; } = new OptionsFactory();

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