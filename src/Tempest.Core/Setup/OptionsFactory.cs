using System;
using Tempest.Core.Options;

namespace Tempest.Core.Setup
{
    public class OptionsFactory : BuilderFactoryBase
    {
        public OptionsFactory(GeneratorBase engine) : base(engine)
        {
        }

        public ListConfigurationOption List(string optionTitle)
        {
            return new ListConfigurationOption(optionTitle);
        }

        public ListConfigurationOption List(string optionTitle, Action<string> action)
        {
            return new ListConfigurationOption(optionTitle, action);
        }

        public InputConfigurationOption Input(string optionTitle, Action<string> action)
        {
            return new InputConfigurationOption(optionTitle, action);
        }

        public ListConfigurationOption List(Func<string> optionTitle)
        {
            return new ListConfigurationOption(optionTitle);
        }

        public ListConfigurationOption List(Func<string> optionTitle, Action<string> resultingAction)
        {
            return new ListConfigurationOption(optionTitle, resultingAction);
        }
    }
}