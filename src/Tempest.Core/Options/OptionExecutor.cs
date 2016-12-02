using System.Collections.Generic;

namespace Tempest.Core.Options
{
    public class OptionExecutor
    {
        public virtual void Execute(IEnumerable<ConfigurationOption> options, string[] selectedOptions)
        {
            List<string> results = new List<string>();
            foreach (var option in options)
            {
                if (option.ShouldRender(results))
                {
                    // This will either be choice from item, or choice that is detected from selectedOptions that matches the item
                    var choice = option.Render();
                    option.ActOn(choice);
                    results.Add(choice);
                }
            }
        }
    }
}