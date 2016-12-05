namespace Tempest
{
    public class TempestRunnerArguments
    {
        public string GeneratorName { get; set; } = "";
        public string[] GeneratorParameters { get; set; } = new string[0];
        public string SearchPath { get; set; } = "";
        public string Verbosity { get; set; } = "";
    }
}