using System;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public class InputConfigurationOption : ConfigurationOption<InputConfigurationOption>
    {
        public InputConfigurationOption(string optionTitle, Action<string> titleAction)
            : base(titleAction, optionTitle)
        {
        }

        protected override OptionRendererBase Renderer => new InputOptionRenderer(this);
    }
}