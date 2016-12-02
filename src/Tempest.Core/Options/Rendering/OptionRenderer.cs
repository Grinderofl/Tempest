namespace Tempest.Core.Options.Rendering
{
    public abstract class OptionRenderer
    {
        protected readonly ConfigurationOption ConfigurationOption;

        protected OptionRenderer(ConfigurationOption configurationOption)
        {
            ConfigurationOption = configurationOption;
            
        }

        /// <summary>
        /// Render the option. Should have a RenderContext to output to - an abstraction around console possibly? at some point
        /// </summary>
        /// <returns></returns>
        public abstract string Render();
    }
}