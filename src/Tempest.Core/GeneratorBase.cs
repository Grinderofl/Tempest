using System.IO;

namespace Tempest.Core
{
    public abstract class GeneratorBase : GeneratorEngineBase
    {
        private string _targetSubDirectory;
        public CreateFactory Create { get; set; } = new CreateFactory();
        public UpdateFactory Update { get; set; } = new UpdateFactory();
        public CopyFactory Copy { get; set; } = new CopyFactory();

        public OptionsFactory Options { get; set; } = new OptionsFactory();

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