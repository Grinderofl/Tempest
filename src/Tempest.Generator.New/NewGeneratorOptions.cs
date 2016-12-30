namespace Tempest.Generator.New
{
    public class NewGeneratorOptions
    {
        public string GeneratorName { get; set; }

        public bool SelfHosted { get; set; }
        public bool IncludeBuildScript { get; set; }
        public BuildScriptTypes BuildScriptTypes { get; set; }
    }
}