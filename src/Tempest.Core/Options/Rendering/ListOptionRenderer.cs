using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;

namespace Tempest.Core.Configuration.Options.Rendering
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
                Console.WriteLine($"{itemIndex}) [{choice.Id}] {choice.Title}");
            }

            int? val = null;

            while (val == null)
            {
                var key = Console.ReadKey();
                try
                {
                    val = int.Parse(key.KeyChar.ToString());
                }
                catch (IndexOutOfRangeException)
                {
                    // Just catch if wrong key
                }
            }

            var foundChoice = optionChoices.ElementAt(val.Value - 1);

            return foundChoice.Id;
        }

        protected override string RenderOptionCore()
        {
            return RenderListOptionCore((ListConfigurationOption) AssociatedOption);
        }
    }
}