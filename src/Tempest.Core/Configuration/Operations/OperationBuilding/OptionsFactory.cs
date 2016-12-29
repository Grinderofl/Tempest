using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;
using Tempest.Core.Options;

namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    // todo: remake this into more like IScaffoldBuilder
    public class OptionsFactory
    {
        public IList<ConfigurationOption> Options { get; } = new List<ConfigurationOption>();

        protected T CreateOption<T>(T option) where T : ConfigurationOption
        {
            Options.Add(option);
            return option;
        }

        public ListConfigurationOption List(string optionTitle)
        {
            return CreateOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(string optionTitle, Action<string> action)
        {
            return CreateOption(new ListConfigurationOption(optionTitle, action));
        }

        public InputConfigurationOption Input(string optionTitle, Action<string> action)
        {
            return CreateOption(new InputConfigurationOption(optionTitle, action));
        }

        public ListConfigurationOption List(Func<string> optionTitle)
        {
            return CreateOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(Func<string> optionTitle, Action<string> resultingAction)
        {
            return CreateOption(new ListConfigurationOption(optionTitle, resultingAction));
        }
    }
}