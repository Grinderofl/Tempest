namespace Tempest.Core.Configuration
{
    public class OptionType
    {
        public static OptionType List = new OptionType(new ListOptionRenderer());
        public static OptionType Input = new OptionType(new InputOptionRenderer());

        public OptionTypeRenderer Renderer { get; }

        private OptionType(OptionTypeRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}