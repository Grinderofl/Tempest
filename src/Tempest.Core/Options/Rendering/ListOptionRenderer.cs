using System;
using System.Collections.Generic;
using System.Linq;

namespace Tempest.Core.Options.Rendering
{
    public class ListOptionRenderer : OptionRendererBase
    {
        public ListOptionRenderer(RenderableOptionBase associatedOption) : base(associatedOption)
        {
        }

        protected virtual string RenderListOptionCore(ListConfigurationOption configurationOption)
        {
            var itemIndex = 0;
            var optionChoices = configurationOption.Choices as IList<OptionChoice> ??
                                configurationOption.Choices.ToList();

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

        protected override string RenderOptionCore()
        {
            return RenderListOptionCore((ListConfigurationOption) AssociatedOption);
        }
    }
}