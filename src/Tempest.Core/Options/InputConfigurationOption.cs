using System;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public class InputConfigurationOption : ConfigurationOption<InputConfigurationOption>
    {
        public InputConfigurationOption(string optionTitle, Action<string> resultingAction) : base(resultingAction, optionTitle)
        {
        }

        protected override OptionRendererBase Renderer => new InputOptionRenderer(this);
    }
}