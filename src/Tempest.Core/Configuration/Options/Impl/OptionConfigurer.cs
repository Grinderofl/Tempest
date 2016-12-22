using System;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;

namespace Tempest.Core.Configuration.Options.Impl
{
    public class OptionConfigurer : IOptionConfigurer
    {
        public OptionsConfiguration Options { get; }

        public OptionConfigurer()
        {
            Options = new OptionsConfiguration();
        }

        protected T AddOption<T>(T option) where T : ConfigurationOption
        {
            Options.Options.Add(option);
            return option;
        }

        public ListConfigurationOption List(string optionTitle)
        {
            return AddOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(string optionTitle, Action<string> action)
        {
            return AddOption(new ListConfigurationOption(optionTitle, action));
        }

        public InputConfigurationOption Input(string optionTitle, Action<string> action)
        {
            return AddOption(new InputConfigurationOption(optionTitle, action));
        }

        public ListConfigurationOption List(Func<string> optionTitle)
        {
            return AddOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(Func<string> optionTitle, Action<string> resultingAction)
        {
            return AddOption(new ListConfigurationOption(optionTitle, resultingAction));
        }
    }
}