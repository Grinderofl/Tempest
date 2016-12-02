using System;
using System.Collections.Generic;
using System.Linq;

namespace Tempest.Core.Options.Rendering
{
    public class ListOptionRenderer : OptionRenderer
    {
        public ListOptionRenderer(ConfigurationOption configurationOption) : base(configurationOption)
        {    
        }

        public override string Render()
        {
            return RenderCore((ListConfigurationOption) ConfigurationOption);
        }

        protected virtual string RenderCore(ListConfigurationOption configurationOption)
        {
            var itemIndex = 0;
            var optionChoices = configurationOption.Choices as IList<OptionChoice> ?? configurationOption.Choices.ToList();

            Console.WriteLine(configurationOption.Title);

            foreach (var choice in optionChoices)
            {
                itemIndex++;
                Console.WriteLine($"{itemIndex}) {choice.Title}");
            }

            var key = Console.ReadKey();
            var val = int.Parse(key.KeyChar.ToString());
            var foundChoice = optionChoices.ElementAt(val - 1);

            return foundChoice.Id;
        }
    }
}