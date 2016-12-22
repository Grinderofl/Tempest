using System;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Rendering;

namespace Tempest.Core.Configuration.Options.Defaults
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