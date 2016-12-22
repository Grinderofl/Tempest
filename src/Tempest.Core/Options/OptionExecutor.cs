using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;

namespace Tempest.Core.Options
{
    public class OptionExecutor
    {
        public virtual void Execute(ConfigurationOption[] options, string[] selectedOptions)
        {
            var results = new List<string>();

            Action<ConfigurationOption, string> actOnOption = (option, choice) =>
            {
                option.ActOn(choice);
                results.Add(choice);
            };

            for (var i = 0; i < options.Length; i++)
            {
                var option = options[i];
                if (!option.ShouldRender(results)) continue;
                if (selectedOptions.Length > i)
                {
                    var launchArgument = selectedOptions[i];
                    if ((launchArgument != null)
                        && option.CanActUpon(launchArgument))
                    {
                        actOnOption(option, launchArgument);
                        continue;
                    }
                    // Maybe we throw something here if we can't find a launch argument because we can't do magic matchup?
                }

                var choice = option.Render();
                actOnOption(option, choice);
            }
        }
    }
}