using System;
using System.Collections.Generic;
using System.Linq;

namespace Tempest.Core.Configuration
{
    public class ListOptionRenderer : OptionTypeRenderer
    {
        public override string Render(OptionItem optionItem)
        {
            return RenderCore(optionItem);
        }

        private static string RenderCore(OptionItem optionItem)
        {
            var itemIndex = 0;
            var optionChoices = optionItem.Choices as IList<OptionChoice> ?? optionItem.Choices.ToList();

            Console.WriteLine(optionItem.Title);

            foreach (var choice in optionChoices)
            {
                itemIndex++;
                Console.WriteLine($"{itemIndex}) {choice.Title}");
            }

            var key = Console.ReadKey();
            var val = int.Parse(key.KeyChar.ToString());
            var foundChoice = optionChoices.ElementAt(val - 1);

            foundChoice.Action?.Invoke();

            optionItem.Action(foundChoice.Id);

            return foundChoice.Id;
        }
    }
}