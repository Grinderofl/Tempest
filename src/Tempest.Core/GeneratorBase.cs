namespace Tempest.Core
{
    public abstract class GeneratorBase : GeneratorEngineBase
    {
        public CreateFactory Create { get; set; } = new CreateFactory();
        public UpdateFactory Update { get; set; } = new UpdateFactory();
        public CopyFactory Copy { get; set; } = new CopyFactory();

        public OptionsFactory Options { get; set; } = new OptionsFactory();
    }
}