namespace Tempest.Core
{
    public class Generator : EngineBase
    {
        public CreateFactory Create { get; set; } = new CreateFactory();
        public UpdateFactory Update { get; set; } = new UpdateFactory();
        public CopyFactory Copy { get; set; } = new CopyFactory();
    }
}