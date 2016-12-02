using System;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public class InputConfigurationOption : ConfigurationOption
    {
        public InputConfigurationOption(string optionTitle, Action<string> action)
        {
            Title = optionTitle;
            Action = action;
        }

        protected override OptionRenderer Renderer => new InputOptionRenderer(this);
    }
}