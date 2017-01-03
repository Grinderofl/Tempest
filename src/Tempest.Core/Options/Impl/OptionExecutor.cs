using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Operations.Transforms;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options.Impl
{
    public class OptionExecutor : IOptionExecutor
    {
        private RenderOptions _renderOptions;

        public OptionExecutor(RenderOptions renderOptions)
        {
            _renderOptions = renderOptions;
        }

        public virtual void Execute(IConfigurationOption[] options, string[] selectedOptions)
        {
            var results = new List<string>();

            Action<IConfigurationOption, string> actOnOption = (option, choice) =>
            {
                option.ActOn(choice);
                results.Add(choice);
            };

            var renderContext = new RenderContext(_renderOptions);

            for (var i = 0; i < options.Length; i++)
            {
                var option = options[i];
                if (!option.ShouldRender(results)) continue;
                if (selectedOptions != null && selectedOptions.Length > i)
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

                var choice = option.Render(renderContext);
                actOnOption(option, choice);
            }
        }
    }
}